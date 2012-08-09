using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GamePedia.Data;
using Windows.UI.Xaml.Data;

namespace GamePedia.DataModel
{
    [System.Runtime.Serialization.DataContract]
    public class GamePediaDataGenre : GamePediaDataGroup
    {
        public GamePediaDataGenre(String uniqueId, String title, String imagePath, String description)
            : base(uniqueId, title, imagePath, description, GroupType.Genre)
        {
            
        }
    }

    public class DataGenreConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var genres = (IEnumerable<GamePediaDataGenre>)value;
            string result = string.Format("{0}", string.Join(Environment.NewLine, genres.Select(x => x.Title)));
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
