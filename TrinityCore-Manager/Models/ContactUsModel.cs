using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catel.Data;

namespace TrinityCore_Manager.Models
{
    public class ContactUsModel : ModelBase
    {

        public ContactUsModel()
        {
        }

        public string Name
        {
            get
            {
                return GetValue<string>(NameProperty);
            }
            set
            {
                SetValue(NameProperty, value);
            }
        }

        public static readonly PropertyData NameProperty = RegisterProperty("Name", typeof(string));

        public string Email
        {
            get
            {
                return GetValue<string>(EmailProperty);
            }
            set
            {
                SetValue(EmailProperty, value);
            }
        }

        public static readonly PropertyData EmailProperty = RegisterProperty("Email", typeof(string));

        public string Subject
        {
            get
            {
                return GetValue<string>(SubjectProperty);
            }
            set
            {
                SetValue(SubjectProperty, value);
            }
        }

        public static readonly PropertyData SubjectProperty = RegisterProperty("Subject", typeof(string));

        public string Message
        {
            get
            {
                return GetValue<string>(MessageProperty);
            }
            set
            {
                SetValue(MessageProperty, value);
            }
        }

        public static readonly PropertyData MessageProperty = RegisterProperty("Message", typeof(string));

    }
}
