using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Catel.Data;
using Catel.MVVM;
using Catel.MVVM.Services;
using TrinityCore_Manager.Models;

namespace TrinityCore_Manager.ViewModels
{
    public class ContactUsViewModel : ViewModelBase
    {

        private readonly IPleaseWaitService _pleaseWaitService;
        private readonly IMessageService _messageService;

        private const string ContactAPI = "http://mitch528.com/api/TCMBugReport";

        public Command SendCommand { get; private set; }

        public ContactUsViewModel(ContactUsModel model, IPleaseWaitService pleaseWaitService, IMessageService messageService)
        {

            ContactUs = model;

            _pleaseWaitService = pleaseWaitService;
            _messageService = messageService;

            SendCommand = new Command(SendMessage);

        }

        public async void SendMessage()
        {

            if (string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Subject) || string.IsNullOrEmpty(Message))
            {

                _messageService.ShowError("All fields are required!");

                return;

            }

            _pleaseWaitService.Show("Sending...");

            using (WebClient client = new WebClient())
            {

                var nvc = new NameValueCollection();

                nvc.Add("Name", Name);
                nvc.Add("Email", Email);
                nvc.Add("Subject", Subject);
                nvc.Add("Message", Message.Replace(Environment.NewLine, "<br/>"));

                try
                {
                    await client.UploadValuesTaskAsync(new Uri(ContactAPI), "POST", nvc);
                }
                catch (Exception ex)
                {
                    _messageService.ShowError(ex.Message);
                }

            }

            _pleaseWaitService.Hide();

            SaveAndCloseViewModel();

        }

        [Model]
        public ContactUsModel ContactUs
        {
            get
            {
                return GetValue<ContactUsModel>(ContactUsProperty);
            }
            set
            {
                SetValue(ContactUsProperty, value);
            }
        }

        public static readonly PropertyData ContactUsProperty = RegisterProperty("ContactUs", typeof(ContactUsModel));

        [ViewModelToModel("ContactUs")]
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

        [ViewModelToModel("ContactUs")]
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

        [ViewModelToModel("ContactUs")]
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

        [ViewModelToModel("ContactUs")]
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
