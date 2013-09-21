using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Catel.Data;
using TrinityCore_Manager.Misc;

namespace TrinityCore_Manager.Models
{
    [AllowNonSerializableMembers]
    public class ItemModel : ModelBase
    {

        public ItemModel(Bitmap itemImage)
        {

            using (MemoryStream memory = new MemoryStream())
            {

                itemImage.Save(memory, ImageFormat.Bmp);
                memory.Position = 0;
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memory;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();

                ItemImage = bitmapImage;

            }

        }

        public ImageSource ItemImage
        {
            get
            {
                return GetValue<ImageSource>(ItemImageProperty);
            }
            set
            {
                SetValue(ItemImageProperty, value);
            }
        }

        public static readonly PropertyData ItemImageProperty = RegisterProperty("ItemImage", typeof(ImageSource));

        public int ItemId
        {
            get
            {
                return GetValue<int>(ItemIdProperty);
            }
            set
            {
                SetValue(ItemIdProperty, value);
            }
        }

        public static readonly PropertyData ItemIdProperty = RegisterProperty("ItemId", typeof(int));

        public string ItemName
        {
            get
            {
                return GetValue<string>(ItemNameProperty);
            }
            set
            {
                SetValue(ItemNameProperty, value);
            }
        }

        public static readonly PropertyData ItemNameProperty = RegisterProperty("ItemName", typeof(string));

    }
}
