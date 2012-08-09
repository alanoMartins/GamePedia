using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GamePedia.Data;

namespace GamePedia.DataModel
{
    /// <summary>
    /// Generic group data model.
    /// </summary>
    [System.Runtime.Serialization.DataContract]
    public abstract class GamePediaDataGroup : GamePediaDataCommon
    {
        public GamePediaDataGroup(String uniqueId, String title, String imagePath, String description, GroupType type)
            : base(uniqueId, title, imagePath, description)
        {
            this.GroupType = type;
        }

        private GroupType _groupType;
        public GroupType GroupType
        {
            get { return _groupType; }
            set { _groupType = value; }
        }

        private ObservableCollection<GamePediaDataItem> _items = new ObservableCollection<GamePediaDataItem>();
        public ObservableCollection<GamePediaDataItem> Items
        {
            get { return this._items; }
        }

        public IEnumerable<GamePediaDataItem> TopItems
        {
            // Provides a subset of the full items collection to bind to from a GroupedItemsPage
            // for two reasons: GridView will not virtualize large items collections, and it
            // improves the user experience when browsing through groups with large numbers of
            // items.
            //
            // A maximum of 12 items are displayed because it results in filled grid columns
            // whether there are 1, 2, 3, 4, or 6 rows displayed
            get { return this._items.Distinct().Take(12); }
        }
    }
}
