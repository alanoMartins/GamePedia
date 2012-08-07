using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GamePedia.Data;

namespace GamePedia.DataModel
{
    public class GamePediaDataGenre : GamePediaDataGroup
    {
        public GamePediaDataGenre(String uniqueId, String title, String imagePath, String description)
            : base(uniqueId, title, imagePath, description, GroupType.Genre)
        {
            
        }

        private ObservableCollection<GamePediaDataItem> _itens = new ObservableCollection<GamePediaDataItem>();
        private ObservableCollection<GamePediaDataItem> Itens
        { 
            get { return this._itens; }
            set { this.SetProperty(ref this._itens, value); }
        }
        public IEnumerable<GamePediaDataItem> TopItens
        {
            get { return this._itens.Take(12); }
        }
    }
}
