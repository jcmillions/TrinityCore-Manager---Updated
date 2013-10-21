// TrinityCore-Manager
// Copyright (C) 2013 Mitchell Kutchuk
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using TrinityCore_Manager.Database.Classes;
using TrinityCore_Manager.Extensions;
using TrinityCore_Manager.Security;
using MySql.Data.Types;
using TrinityCore_Manager.Database.Enums;

namespace TrinityCore_Manager.Database
{
    class AuthDatabase : MySqlDatabase
    {

        public AuthDatabase(string serverHost, int port, string username, string password, string dbName)
            : base(serverHost, port, username, password, dbName)
        {
        }

        public async Task<bool> CreateAccount(string username, string password, int gmlevel, int expansion, string email = "")
        {

            DataTable dt = await ExecuteQuery("SELECT id FROM `account` WHERE username = @username", new MySqlParameter("@username", username));

            if (dt.Rows.Count > 0)
                return false;

            await ExecuteNonQuery("INSERT INTO `account` (`username`, `sha_pass_hash`, `expansion`, `email`) VALUES (@username, @hash, @expansion, @email)",
                new MySqlParameter("@username", username), new MySqlParameter("@hash", String.Format("{0}:{1}", username.ToUpper(), password.ToUpper()).ToSHA1()),
                new MySqlParameter("@expansion", expansion), new MySqlParameter("@email", email));


            dt = await ExecuteQuery("SELECT id FROM `account` WHERE username = @username", new MySqlParameter("@username", username));

            if (dt.Rows.Count != 1)
                return false;

            await ExecuteNonQuery("INSERT INTO `account_access` (`id`, `gmlevel`) VALUES (@id, @gmlevel)", new MySqlParameter("@id", (int)dt.Rows[0]["id"]), new MySqlParameter("@gmlevel", gmlevel));

            return true;

        }

        /// <summary>
        /// Modify an account
        /// </summary>
        /// <param name="id">The id of the account</param>
        /// <param name="username">The new username for the account</param>
        /// <param name="password">The new password for the account</param>
        /// <param name="email">The new email for the account</param>
        /// <param name="lvl">The new GM level for the account</param>
        /// <param name="exp">The new expansion for the account</param>
        /// <returns>An awaitable Task object</returns>
        public async Task EditAccount(int id, string username, string password, string email, GMLevel lvl, Expansion exp)
        {

            await ExecuteNonQuery("UPDATE `account` SET username=@username, sha_pass_hash=@hash, expansion=@expansion, email=@email WHERE id=@id",
                new MySqlParameter("@username", username), new MySqlParameter("@hash", String.Format("{0}:{1}", username.ToUpper(), password.ToUpper()).ToSHA1()),
                new MySqlParameter("@expansion", (int)exp), new MySqlParameter("@email", email), new MySqlParameter("@id", id));

            await ExecuteNonQuery("UPDATE `account_access` SET gmlevel=@gmlevel WHERE id = @id", new MySqlParameter("@gmlevel", (int)lvl), new MySqlParameter("@id", id));

        }


        public async Task<GMLevel> GetAccountAccess(int accountId)
        {

            DataTable dt = await ExecuteQuery("SELECT `gmlevel` FROM `account_access` WHERE `id` = @id", new MySqlParameter("@id", accountId));

            if (dt.Rows.Count == 0)
                return GMLevel.Player;

            DataRow row = dt.Rows[0];

            int level = (byte)row["gmlevel"];

            return (GMLevel)level;

        }

        private Account BuildAccount(DataRow row)
        {

            int id = row["id"] != DBNull.Value ? (int)row["id"] : -1;
            string username = row["username"] != DBNull.Value ? (string)row["username"] : String.Empty;
            string shaHash = row["sha_pass_hash"] != DBNull.Value ? (string)row["sha_pass_hash"] : String.Empty;
            string email = row["email"] != DBNull.Value ? (string)row["email"] : String.Empty;
            DateTime joinDate = row["joindate"] != DBNull.Value && ((MySqlDateTime)row["joindate"]).IsValidDateTime ? ((MySqlDateTime)row["joindate"]).GetDateTime() : DateTime.Now;
            string lastip = row["last_ip"] != DBNull.Value ? (string)row["last_ip"] : String.Empty;
            int failedLogins = row["failed_logins"] != DBNull.Value ? (int)((uint)row["failed_logins"]) : 0;
            int locked = row["locked"] != DBNull.Value ? ((byte)row["locked"]) : 0;
            string lockCountry = row["lock_country"] != DBNull.Value ? (string)row["lock_country"] : String.Empty;
            DateTime lastLogin = row["last_login"] != DBNull.Value && ((MySqlDateTime)row["last_login"]).IsValidDateTime ? ((MySqlDateTime)row["last_login"]).GetDateTime() : DateTime.Now;
            bool online = row["online"] != DBNull.Value && Convert.ToBoolean(row["online"]);
            int exp = row["expansion"] != DBNull.Value ? ((byte)row["expansion"]) : 0;

            var acct = new Account();
            acct.Id = id;
            acct.Username = username;
            acct.Hash = shaHash;
            acct.Email = email;
            acct.JoinDate = joinDate;
            acct.LastIp = lastip;
            acct.FailedLogins = failedLogins;
            acct.Locked = locked;
            acct.LockCountry = lockCountry;
            acct.LastLogin = lastLogin;
            acct.Online = online;
            acct.Exp = (Expansion)exp;

            return acct;

        }

        public async Task<Account> GetAccount(int acctId)
        {

            DataTable dt = await ExecuteQuery("SELECT * FROM `account` WHERE id = @id;", new MySqlParameter("@id", acctId));

            if (dt.Rows.Count == 0)
                return null;

            DataRow row = dt.Rows[0];

            return BuildAccount(row);

        }

        public async Task<List<Account>> GetOnlineAccounts()
        {

            DataTable dt = await ExecuteQuery("SELECT * FROM `account` WHERE Online = 1");

            List<Account> accounts = new List<Account>();

            foreach (DataRow dr in dt.Rows)
            {
                BuildAccount(dr);
            }

            return accounts;

        }

        public async Task<Account> GetAccount(string username)
        {

            DataTable dt = await ExecuteQuery("SELECT * FROM `account` WHERE username = @username", new MySqlParameter("@username", username));

            if (dt.Rows.Count == 0)
                return null;

            DataRow row = dt.Rows[0];

            return BuildAccount(row);

        }

        public async Task<List<Account>> GetAccounts()
        {

            DataTable dt = await ExecuteQuery("SELECT * FROM `account`");

            var accts = new List<Account>();

            foreach (DataRow row in dt.Rows)
            {
                accts.Add(BuildAccount(row));
            }

            return accts;

        }

        public async Task<List<Account>> SearchForAccount(string like)
        {

            DataTable dt = await ExecuteQuery("SELECT * FROM `account` WHERE LOWER(username) LIKE '%" + like.ToLower() + "%' LIMIT 15");

            var accts = new List<Account>();

            foreach (DataRow row in dt.Rows)
            {
                accts.Add(BuildAccount(row));
            }

            return accts;

        }

        public async Task<List<BannedAccount>> SearchForBannedAccount(string like)
        {

            List<Account> accountsLike = await SearchForAccount(like);

            List<BannedAccount> bannedAccountsLike = new List<BannedAccount>();

            foreach (var account in accountsLike)
            {

                BannedAccount ban = await GetBannedAccount(account.Id);

                if (ban == null)
                    continue;

                if (ban.Active)
                    continue;

                bannedAccountsLike.Add(ban);

            }

            return bannedAccountsLike;

        }

        public async Task<List<BannedAccount>> GetBannedAccounts()
        {

            DataTable dt = await ExecuteQuery("SELECT * FROM `account_banned` WHERE active = 1;");

            List<BannedAccount> bannedAccounts = new List<BannedAccount>();

            foreach (DataRow row in dt.Rows)
            {

                var bannedAccount = BuildAccountBan(row);

                bannedAccounts.Add(bannedAccount);

            }

            return bannedAccounts;

        }

        public async Task<BannedAccount> GetBannedAccount(int userId)
        {

            DataTable dt = await ExecuteQuery("SELECT * FROM `account_banned` WHERE active = 1 AND id=@id", new MySqlParameter("@id", userId));

            if (dt.Rows.Count == 0)
                return null;

            return BuildAccountBan(dt.Rows[0]);

        }

        private BannedAccount BuildAccountBan(DataRow row)
        {

            int id = Convert.ToInt32((uint)row["id"]);
            DateTime banDate = Convert.ToInt64(((uint)row["bandate"])).ToDateTime();
            DateTime unbanDate = Convert.ToInt64(((uint)row["unbandate"])).ToDateTime();
            string bannedby = (string)row["bannedby"];
            string banreason = (string)row["banreason"];
            bool active = ((byte)row["active"]) == 1;

            BannedAccount bannedAccount = new BannedAccount();
            bannedAccount.Id = id;
            bannedAccount.BanDate = banDate;
            bannedAccount.UnbanDate = unbanDate;
            bannedAccount.BannedBy = bannedby;
            bannedAccount.BanReason = banreason;
            bannedAccount.Active = active;

            return bannedAccount;

        }

        public async Task ChangeAccountPassword(int accountId, string password)
        {

            Account acct = await GetAccount(accountId);

            if (acct == null)
                return;

            if (!String.IsNullOrEmpty(password))
                await ExecuteNonQuery("UPDATE `account` SET sha_pass_hash = @password, sessionkey = '', v = '', s = '' WHERE `id` = @id;", new MySqlParameter("@password", (acct.Username.ToUpper() + ":" + password.ToUpper()).ToSHA1()), new MySqlParameter("@id", accountId));

        }

        public async Task ChangeAccountPassword(string username, string password)
        {

            Account acct = await GetAccount(username);

            if (acct == null)
                return;

            await ChangeAccountPassword(acct.Id, password);

        }

        public async Task ChangeAccountExpansion(int accountId, Expansion exp)
        {

            if (await GetAccount(accountId) == null)
                return;

            await ExecuteNonQuery("UPDATE `account` SET expansion = @expansion, sessionkey = '', v = '', s = '' WHERE `id` = @id;", new MySqlParameter("@expansion", (int)exp), new MySqlParameter("@id", accountId));

        }

        public async Task SetAccountLock(int accountId, bool locked)
        {
            if (await GetAccount(accountId) == null)
                return;

            await ExecuteNonQuery("UPDATE `account` SET locked = @locked WHERE `id` = @id", new MySqlParameter("@locked", locked), new MySqlParameter("@id", accountId));
        }

        public async Task SetAccountLock(string username, bool locked)
        {
            Account account = await GetAccount(username);

            if (account == null)
                return;

            await SetAccountLock(account.Id, locked);
        }

        public async Task ChangeAccountExpansion(string username, Expansion exp)
        {

            Account acct = await GetAccount(username);

            if (acct == null)
                return;

            await ChangeAccountExpansion(acct.Id, exp);

        }

        public async Task SetGMLevel(int accountId, GMLevel level, int realmid)
        {
            await ExecuteNonQuery("UPDATE `account_access` SET gmlevel = @gmlevel, RealmID = @realmid WHERE `id` = @id;", new MySqlParameter("@gmlevel", (int)level), new MySqlParameter("@realmid", realmid), new MySqlParameter("@id", accountId));
        }

        public async Task SetGMLevel(string username, GMLevel level, int realmid)
        {

            Account account = await GetAccount(username);

            if (account == null)
                return;

            await SetGMLevel(account.Id, level, realmid);

        }

        public async Task<int> GetPlayersOnlineCount()
        {

            object online = await ExecuteScalar("SELECT COUNT(*) FROM `account` WHERE `online` = 1");

            return online != null ? Convert.ToInt32(online) : 0;

        }

        public async Task<int> CleanupAccounts(DateTime lastLogin)
        {

            int acctLenBefore = Convert.ToInt32(await ExecuteScalar("SELECT COUNT(*) FROM `account`"));

            await ExecuteNonQuery("DELETE FROM `auth`.`account` WHERE `last_login` < @lastlogin", new MySqlParameter("@lastlogin", lastLogin.ToString("yyyy-MM-dd")));

            int acctLenAfter = Convert.ToInt32(await ExecuteScalar("SELECT COUNT(*) FROM `account`"));

            return acctLenBefore - acctLenAfter;

            //await ExecuteNonQuery("DELETE FROM `auth`.`account` WHERE `last_login` < @date AND `last_login` <> '0000-00-00 00:00:00';", new MySqlParameter("@date", new MySqlDateTime(date)));

        }

        public Task BanIp(string ip, int bandate, int unbandate, string bannedby, string banreason)
        {
            return ExecuteNonQuery("INSERT INTO `ip_banned` (`ip`, `bandate`, `unbandate`, `bannedby`, `banreason`) VALUES (@ip, @bandate, @unbandate, @bannedby, @banreason)", new MySqlParameter("@ip", ip), new MySqlParameter("@bandate", bandate), new MySqlParameter("@unbandate", unbandate), new MySqlParameter("@bannedby", bannedby), new MySqlParameter("@banreason", banreason));
        }

        public async Task BanAccount(int id, int bandate, int unbandate, string bannedBy, string banReason)
        {
            await ExecuteNonQuery("INSERT INTO `account_banned` (id, bandate, unbandate, bannedby, banreason, active) VALUES (@id, @bandate, @unbandate, @bannedby, @banreason, @active)",
                new MySqlParameter("@id", id), new MySqlParameter("@bandate", bandate), new MySqlParameter("@unbandate", unbandate), new MySqlParameter("@bannedby", bannedBy),
                new MySqlParameter("@banreason", banReason), new MySqlParameter("active", "1"));
        }

        public async Task BanAccount(string username, int bandate, int unbandate, string bannedBy, string banReason)
        {
            Account account = await GetAccount(username);

            if (account == null)
                return;

            await BanAccount(account.Id, bandate, unbandate, bannedBy, banReason);
        }

        public async Task RemoveAccountBan(int id)
        {
            await ExecuteNonQuery("DELETE FROM `account_banned` WHERE `id` = @id", new MySqlParameter("@id", id));
            //await ExecuteNonQuery("UPDATE `account_banned` SET `active` = 0 WHERE `id` = @id", new MySqlParameter("@id", id));
        }

        public async Task AddIpBan(string ip, DateTime bandate, DateTime unbandate, string bannedby, string banreason)
        {
            await ExecuteNonQuery("INSERT INTO `ip_banned` (ip, bandate, unbandate, bannedby, banreason) VALUES (@ip, @bandate, @unbandate, @bannedby, @banreason)",
                new MySqlParameter("@ip", ip), new MySqlParameter("@bandate", bandate.ToUnixTimestamp()), new MySqlParameter("@unbandate", unbandate.ToUnixTimestamp()),
                new MySqlParameter("@bannedby", bannedby), new MySqlParameter("@banreason", banreason));
        }

        public async Task RemoveIpBan(string ip)
        {
            await ExecuteNonQuery("DELETE FROM `ip_banned` WHERE `ip` = @ip", new MySqlParameter("@ip", ip));
        }

        private IPBan BuildIPBan(DataRow row)
        {

            IPBan ipBan = new IPBan();
            ipBan.IP = (string)row["ip"];
            ipBan.BanDate = Convert.ToInt64((uint)row["bandate"]).ToDateTime();
            ipBan.UnbanDate = Convert.ToInt64((uint)row["unbandate"]).ToDateTime();
            ipBan.BannedBy = (string)row["bannedby"];
            ipBan.BanReason = (string)row["banreason"];

            return ipBan;

        }

        public async Task<List<IPBan>> GetIPBans()
        {

            DataTable dt = await ExecuteQuery("SELECT * FROM `ip_banned`");

            List<IPBan> ipBans = new List<IPBan>();

            foreach (DataRow dr in dt.Rows)
            {
                ipBans.Add(BuildIPBan(dr));
            }

            return ipBans;

        }

        public async Task<IPBan> GetIPBan(string ip)
        {

            DataTable dt = await ExecuteQuery("SELECT * FROM `ip_banned` WHERE ip = @ip", new MySqlParameter("@ip", ip));

            if (dt.Rows.Count == 0)
                return null;

            return BuildIPBan(dt.Rows[0]);

        }

    }
}
