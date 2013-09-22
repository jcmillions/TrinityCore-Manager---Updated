using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using Catel.Data;
using Catel.MVVM;
using Catel.MVVM.Services;
using TrinityCore_Manager.Misc;
using TrinityCore_Manager.Models;
using TrinityCore_Manager.TCM;

namespace TrinityCore_Manager.ViewModels
{
    public class FindItemViewModel : ViewModelBase
    {

        private int _page;

        private readonly IUIVisualizerService _uiVisualizerService;
        private readonly IPleaseWaitService _pleaseWaitService;
        private readonly IMessageService _messageService;

        public Command SearchCommand { get; private set; }

        public Command NextPageCommand { get; private set; }

        public Command PreviousPageCommand { get; private set; }

        public Command OkCommand { get; private set; }

        public FindItemViewModel(FindItemModel model, IUIVisualizerService uiVisualizerService, IPleaseWaitService pleaseWaitService, IMessageService messageService)
        {

            FindItem = model;

            _uiVisualizerService = uiVisualizerService;
            _pleaseWaitService = pleaseWaitService;
            _messageService = messageService;

            SearchCommand = new Command(Search);
            NextPageCommand = new Command(NextPage);
            PreviousPageCommand = new Command(PreviousPage);
            OkCommand = new Command(Ok);

        }

        private void Ok()
        {
            SaveAndCloseViewModel();
        }

        private async void PreviousPage()
        {
            await SearchForItem(--_page);
        }

        private async void NextPage()
        {
            await SearchForItem(++_page);
        }

        private async void Search()
        {

            _page = 0;
            Page = "Page 0 of 0";

            await SearchForItem(_page);

        }

        private async Task SearchForItem(int page)
        {

            if (page < 0)
                return;

            _pleaseWaitService.Show("Loading...");

            Dictionary<int, string> items = await TCManager.Instance.WorldDatabase.SearchForItem(SearchText, page);

            int totalPages = await TCManager.Instance.WorldDatabase.GetTotalPagesForItem(SearchText);

            if (page > totalPages)
            {

                _pleaseWaitService.Hide();

                return;

            }

            Page = "Page " + (page + 1) + " of " + (totalPages + 1);

            var itemModels = new List<ItemModel>();

            foreach (var item in items)
            {

                try
                {
                    WoWItem wowItem = await WoWItem.GetItem(item.Key);
                    Image itemIcon = await wowItem.GetIconTaskAsync();
                    itemModels.Add(new ItemModel(new Bitmap(itemIcon)) { ItemId = item.Key, ItemName = item.Value });
                }
                catch (Exception)
                {
                    itemModels.Add(new ItemModel(new Bitmap(WoWItem.UnknownImage)) { ItemId = item.Key, ItemName = item.Value });
                }
            }

            Items = new ObservableCollection<ItemModel>(itemModels);

            _pleaseWaitService.Hide();

        }

        [Model]
        public FindItemModel FindItem
        {
            get
            {
                return GetValue<FindItemModel>(FindItemProperty);
            }
            set
            {
                SetValue(FindItemProperty, value);
            }
        }

        public static readonly PropertyData FindItemProperty = RegisterProperty("FindItem", typeof(FindItemModel));

        [ViewModelToModel("FindItem")]
        public ObservableCollection<ItemModel> Items
        {
            get
            {
                return GetValue<ObservableCollection<ItemModel>>(ItemsProperty);
            }
            set
            {
                SetValue(ItemsProperty, value);
            }
        }

        public static readonly PropertyData ItemsProperty = RegisterProperty("Items", typeof(ObservableCollection<ItemModel>));

        [ViewModelToModel("FindItem")]
        public string SearchText
        {
            get
            {
                return GetValue<string>(SearchTextProperty);
            }
            set
            {
                SetValue(SearchTextProperty, value);
            }
        }

        public static readonly PropertyData SearchTextProperty = RegisterProperty("SearchText", typeof(string));

        public string Page
        {
            get
            {
                return GetValue<string>(PageProperty);
            }
            set
            {
                SetValue(PageProperty, value);
            }
        }

        public static readonly PropertyData PageProperty = RegisterProperty("Page", typeof(string), "Page 0 of 0");

        public ItemModel SelectedItem
        {
            get
            {
                return GetValue<ItemModel>(SelectedItemProperty);
            }
            set
            {
                SetValue(SelectedItemProperty, value);
            }

        }

        public static readonly PropertyData SelectedItemProperty = RegisterProperty("SelectedItem", typeof(ItemModel));

    }
}
