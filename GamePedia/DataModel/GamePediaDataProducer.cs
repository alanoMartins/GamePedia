using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GamePedia.Data;

namespace GamePedia.DataModel
{
    public class GamePediaDataProducer : GamePediaDataGroup
    {
        public GamePediaDataProducer(String uniqueId, String title, String imagePath, String description, String content, params GamePediaDataConsole[] consoles)
            : base(uniqueId, title, imagePath, description, GroupType.Producer)
        {
            this._content = content;
        }

        private string _content = string.Empty;
        public string Content
        {
            get { return this._content; }
            set { this.SetProperty(ref this._content, value); }
        }

        private ObservableCollection<GamePediaDataConsole> _consoles = new ObservableCollection<GamePediaDataConsole>();
        public ObservableCollection<GamePediaDataConsole> Consoles
        {
            get { return this._consoles; }
            set { this.SetProperty(ref this._consoles, value); }
        }

        public IEnumerable<GamePediaDataConsole> TopConsoles
        {
            get { return this._consoles.Take(12); }
        }
    }
}
