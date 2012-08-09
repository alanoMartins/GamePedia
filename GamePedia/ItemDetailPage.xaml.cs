using GamePedia.Data;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using GamePedia.DataModel;
using GamePedia.Common;

using Windows.ApplicationModel.DataTransfer;
using System.Text;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media.Imaging;
// The Item Detail Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234232

namespace GamePedia
{
    /// <summary>
    /// A page that displays details for a single item within a group while allowing gestures to
    /// flip through other items belonging to the same group.
    /// </summary>
    public sealed partial class ItemDetailPage : GamePedia.Common.LayoutAwarePage
    {
        private KeyValuePair<string, string> parameters;
        public ItemDetailPage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Populates the page with content passed during navigation.  Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="navigationParameter">The parameter value passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested.
        /// </param>
        /// <param name="pageState">A dictionary of state preserved by this page during an earlier
        /// session.  This will be null the first time a page is visited.</param>
        protected override void LoadState(Object navigationParameter, Dictionary<String, Object> pageState)
        {
            DataTransferManager.GetForCurrentView().DataRequested += ItemDetailPage_DataRequested;
            // Allow saved page state to override the initial item to display
            if (pageState != null && pageState.ContainsKey("SelectedItem"))
            {
                navigationParameter = pageState["SelectedItem"];
            }
            this.parameters = (KeyValuePair<string, string>)navigationParameter;
            // TODO: Create an appropriate data model for your problem domain to replace the sample data
            var item = GamePediaDataSource.GetItem(this.parameters.Key);
            if (!string.IsNullOrEmpty(this.parameters.Value))
            {
                var group = GamePediaDataSource.GetGroup(this.parameters.Value);
                this.DefaultViewModel["Group"] = group;
                this.DefaultViewModel["Items"] = group.Items.Distinct();
            }
            else
            {
                this.DefaultViewModel["Items"] = new GamePediaDataItem[1] { GamePediaDataSource.GetItem(parameters.Key) };
            }
            this.flipView.SelectedItem = item;
        }

        private void ItemDetailPage_DataRequested(DataTransferManager sender, DataRequestedEventArgs args)
        {
            var request = args.Request;
            var item = (GamePediaDataItem)this.flipView.SelectedItem;
            request.Data.Properties.Title = "Share your favorites games";

            //Share text and description
            var game = "\n\rGAME\r\n";
            game = string.Join("\n\r", item.Title);
            game = ("\r\n\rDESCRIPTION\r\n" + item.Description);
            request.Data.SetText(game);

            //Share imagens
            //ToDo:Verificar a path do item
            var reference = RandomAccessStreamReference.CreateFromUri(((BitmapImage)item.Image).UriSource);  //(new Uri( , UriKind.RelativeOrAbsolute));
            request.Data.Properties.Thumbnail = reference;
            request.Data.SetBitmap(reference);
            request.Data.SetBitmap(reference);
        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="pageState">An empty dictionary to be populated with serializable state.</param>
        protected override void SaveState(Dictionary<String, Object> pageState)
        {
            var selectedItem = (GamePediaDataItem)this.flipView.SelectedItem;
            this.parameters = new KeyValuePair<string,string>(this.parameters.Key, selectedItem.UniqueId);
            pageState["SelectedItem"] = this.parameters;

            DataTransferManager.GetForCurrentView().DataRequested -= ItemDetailPage_DataRequested;
        }
    }
}
