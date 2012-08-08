using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GamePedia.Data;

namespace GamePedia.DataModel
{
    public class GamePediaDataGroupBase : GamePediaDataGroup
    {
        public GamePediaDataGroupBase(String uniqueId, String title, String imagePath, String description, String content, params GamePediaDataConsole[] consoles)
            : base(uniqueId, title, imagePath, description, GroupType.GroupBase)
        {
            this._content = content;
        }

        private string _content = string.Empty;
        public string Content
        {
            get { return this._content; }
            set { this.SetProperty(ref this._content, value); }
        }

        private ObservableCollection<GamePediaDataGroup> _groups = new ObservableCollection<GamePediaDataGroup>();
        public ObservableCollection<GamePediaDataGroup> Groups
        {
            get { return this._groups; }
            set { this.SetProperty(ref this._groups, value); }
        }

        public IEnumerable<GamePediaDataGroup> TopGroups
        {
            get { return this._groups.Take(12); }
        }
    }
}
