using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using GamePedia.Data;

namespace GamePedia.DataModel
{
    /// <summary>
    /// Generic item data model.
    /// </summary>
    [DataContract]
    public class GamePediaDataItem : GamePediaDataCommon
    {
        public GamePediaDataItem(String uniqueId, String title, String imagePath, String description, String content, GamePediaDataGroupBase producer, params GamePediaDataGroup[] groups)
            : base(uniqueId, title, imagePath, description)
        {
            this._content = content;
            this._groups = new ObservableCollection<GamePediaDataGroup>(groups);
            this._producer = producer;
            foreach (var group in _groups)
            {
                if (!group.Items.Contains(this))
                    group.Items.Add(this);
            }
            this._producer.Items.Add(this);
        }
        private GamePediaDataGroupBase _producer;
        
        
        public GamePediaDataGroupBase Producer
        {
            get { return _producer; }
            set { _producer = value; }
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
        public ObservableCollection<GamePediaDataGenre> TopGenres
        {
            get { return new ObservableCollection<GamePediaDataGenre>(this._groups.Where(x => x is GamePediaDataGenre).Cast<GamePediaDataGenre>().Take(12)); }
        }
        public ObservableCollection<GamePediaDataConsole> TopConsoles
        {
            get { return new ObservableCollection<GamePediaDataConsole>(this._groups.Where(x => x is GamePediaDataConsole).Cast<GamePediaDataConsole>().Take(12)); }
        }
    }
}
