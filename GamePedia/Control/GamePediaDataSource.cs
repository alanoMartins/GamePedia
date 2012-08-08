using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.ApplicationModel.Resources.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using GamePedia.DataModel;

// The data model defined by this file serves as a representative example of a strongly-typed
// model that supports notification when members are added, removed, or modified.  The property
// names chosen coincide with data bindings in the standard item templates.
//
// Applications may use this model as a starting point and build on it, or discard it entirely and
// replace it with something appropriate to their needs.

namespace GamePedia.Data
{
    public enum GroupType
    {
        Genre,
        Console,
        GroupBase,
    }

    /// <summary>
    /// Creates a collection of groups and items with hard-coded content.
    /// </summary>
    public sealed class GamePediaDataSource
    {
        private static GamePediaDataSource _GamePediaDataSource = new GamePediaDataSource();

        private ObservableCollection<GamePediaDataGroup> _allGroups = new ObservableCollection<GamePediaDataGroup>();
        public ObservableCollection<GamePediaDataGroup> AllGroups
        {
            get { return this._allGroups; }
        }

        public ObservableCollection<GamePediaDataConsole> AllConsoles
        {
            get
            {
                var consoles = this._allGroups.Where(x => x is GamePediaDataConsole).Cast<GamePediaDataConsole>();
                return new ObservableCollection<GamePediaDataConsole>(consoles);
            }
        }

        public ObservableCollection<GamePediaDataGenre> AllGenres
        {
            get
            {
                var genres = this._allGroups.Where(x => x is GamePediaDataGenre).Cast<GamePediaDataGenre>();
                return new ObservableCollection<GamePediaDataGenre>(genres);
            }
        }

        public static IEnumerable<GamePediaDataGroup> GetGroups(string uniqueId)
        {
            if (!uniqueId.Equals("AllGroups")) throw new ArgumentException("Only 'AllGroups' is supported as a collection of groups");
            return _GamePediaDataSource._allGroups;
        }

        public ObservableCollection<GamePediaDataGroupBase> AllProducers
        {
            get
            {
                var producers = this._allGroups.Where(x => x is GamePediaDataGroupBase).Cast<GamePediaDataGroupBase>();
                return new ObservableCollection<GamePediaDataGroupBase>(producers);
            }
        }

        public static GamePediaDataGenre GetGenre(string uniqueId)
        {
            return _GamePediaDataSource.AllGenres.FirstOrDefault(x => x.UniqueId == uniqueId);
        }

        public static IEnumerable<GamePediaDataGenre> GetGenres(string uniqueId)
        {
            if (!uniqueId.Equals("AllGroups")) throw new ArgumentException("Only 'AllGroups' is supported as a collection of groups");

            return _GamePediaDataSource.AllGenres;
        }

        public static GamePediaDataConsole GetConsole(string uniqueId)
        {
            return _GamePediaDataSource.AllConsoles.FirstOrDefault(x => x.UniqueId == uniqueId);
        }

        public static GamePediaDataGroupBase GetProducter(string uniqueId)
        {
            return _GamePediaDataSource.AllProducers.FirstOrDefault(x => x.UniqueId == uniqueId);
        }

        public static IEnumerable<GamePediaDataGroupBase> GetProducers(string uniqueId)
        {
            if (!uniqueId.Equals("AllGroups")) throw new ArgumentException("Only 'AllGroups' is supported as a collection of groups");

            return _GamePediaDataSource.AllProducers;
        }

        public static GamePediaDataGroup GetGroup(string uniqueId)
        {
            // Simple linear search is acceptable for small data sets
            var matches = _GamePediaDataSource.AllGroups.Where((group) => group.UniqueId.Equals(uniqueId));
            if (matches.Count() == 1) return matches.First();
            return null;
        }

        public static GamePediaDataItem GetItem(string uniqueId)
        {
            // Simple linear search is acceptable for small data sets
            var matches = _GamePediaDataSource.AllGroups.SelectMany(group => group.Items).Where((item) => item.UniqueId.Equals(uniqueId));
            return matches.FirstOrDefault();
        }

        public GamePediaDataSource()
        {
            String ITEM_CONTENT = String.Format("Item Content: {0}\n\n{0}\n\n{0}\n\n{0}\n\n{0}\n\n{0}\n\n{0}",
                        "Curabitur class aliquam vestibulum nam curae maecenas sed integer cras phasellus suspendisse quisque donec dis praesent accumsan bibendum pellentesque condimentum adipiscing etiam consequat vivamus dictumst aliquam duis convallis scelerisque est parturient ullamcorper aliquet fusce suspendisse nunc hac eleifend amet blandit facilisi condimentum commodo scelerisque faucibus aenean ullamcorper ante mauris dignissim consectetuer nullam lorem vestibulum habitant conubia elementum pellentesque morbi facilisis arcu sollicitudin diam cubilia aptent vestibulum auctor eget dapibus pellentesque inceptos leo egestas interdum nulla consectetuer suspendisse adipiscing pellentesque proin lobortis sollicitudin augue elit mus congue fermentum parturient fringilla euismod feugiat");

            var sony = new GamePediaDataGroupBase(Guid.NewGuid().ToString(),
                "Sony", "Assets/Producer/sony(1).jpg", "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vivamus tempor scelerisque lorem in vehicula. Aliquam tincidunt, lacus ut sagittis tristique, turpis massa volutpat augue, eu rutrum ligula ante a ante", "Sony Content");


            var playStation3 = new GamePediaDataConsole(Guid.NewGuid().ToString(),
                    "Playstation 3",
                    "Assets/Console/play3(1).jpg",
                    "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vivamus tempor scelerisque lorem in vehicula. Aliquam tincidunt, lacus ut sagittis tristique, turpis massa volutpat augue, eu rutrum ligula ante a ante");
            var playStation2 = new GamePediaDataConsole(Guid.NewGuid().ToString(),
                    "Playstation 2",
                    "Assets/Console/play2(1).jpg",
                    "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vivamus tempor scelerisque lorem in vehicula. Aliquam tincidunt, lacus ut sagittis tristique, turpis massa volutpat augue, eu rutrum ligula ante a ante");
            var playStation = new GamePediaDataConsole(Guid.NewGuid().ToString(),
                    "Playstation",
                    "Assets/Console/play1(1).jpg",
                    "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vivamus tempor scelerisque lorem in vehicula. Aliquam tincidunt, lacus ut sagittis tristique, turpis massa volutpat augue, eu rutrum ligula ante a ante");


            sony.Groups.Add(playStation3);
            sony.Groups.Add(playStation2);
            sony.Groups.Add(playStation);

            var microsoft = new GamePediaDataGroupBase(Guid.NewGuid().ToString(),
                "Microsoft", "Assets/Producer/microsoft.jpg", "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vivamus tempor scelerisque lorem in vehicula. Aliquam tincidunt, lacus ut sagittis tristique, turpis massa volutpat augue, eu rutrum ligula ante a ante", "Microsoft Content");

            var xBox360 = new GamePediaDataConsole(Guid.NewGuid().ToString(),
                    "XBox 360",
                    "Assets/Console/xbox360(1).jpg",
                    "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vivamus tempor scelerisque lorem in vehicula. Aliquam tincidunt, lacus ut sagittis tristique, turpis massa volutpat augue, eu rutrum ligula ante a ante");

            microsoft.Groups.Add(xBox360);

            var genres = new GamePediaDataGroupBase(Guid.NewGuid().ToString(),
                "Gêneros", "Assets/Genre/genres.png", "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vivamus tempor scelerisque lorem in vehicula. Aliquam tincidunt, lacus ut sagittis tristique, turpis massa volutpat augue, eu rutrum ligula ante a ante", "Microsoft Content");


            var action = new GamePediaDataGenre(
                Guid.NewGuid().ToString(),
                "Action",
                "Assets/Genre/action.png",
                "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vivamus tempor scelerisque lorem in vehicula. Aliquam tincidunt, lacus ut sagittis tristique, turpis massa volutpat augue, eu rutrum ligula ante a ante");
            var adventure = new GamePediaDataGenre(
                Guid.NewGuid().ToString(),
                "Adventure",
                "Assets/Genre/adventure.png",
                "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vivamus tempor scelerisque lorem in vehicula. Aliquam tincidunt, lacus ut sagittis tristique, turpis massa volutpat augue, eu rutrum ligula ante a ante");
            var rpg = new GamePediaDataGenre(
                Guid.NewGuid().ToString(),
                "RPG",
                "Assets/Genre/rpg.png",
                "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vivamus tempor scelerisque lorem in vehicula. Aliquam tincidunt, lacus ut sagittis tristique, turpis massa volutpat augue, eu rutrum ligula ante a ante");
            var race = new GamePediaDataGenre(
                Guid.NewGuid().ToString(),
                "Race",
                "Assets/Genre/race.png",
                "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vivamus tempor scelerisque lorem in vehicula. Aliquam tincidunt, lacus ut sagittis tristique, turpis massa volutpat augue, eu rutrum ligula ante a ante");

            genres.Groups.Add(action);
            genres.Groups.Add(adventure);
            genres.Groups.Add(rpg);
            genres.Groups.Add(race);


            var gow3 = new GamePediaDataItem(Guid.NewGuid().ToString(),
                    "God of War 3",
                    "Assets/Item/gow3(4).jpg",
                    "Item Description: Pellentesque porta, mauris quis interdum vehicula, urna sapien ultrices velit, nec venenatis dui odio in augue. Cras posuere, enim a cursus convallis, neque turpis malesuada erat, ut adipiscing neque tortor ac erat.",
                    ITEM_CONTENT,
                    sony, action, playStation3);

            var asura = new GamePediaDataItem(Guid.NewGuid().ToString(),
                    "Asura's Wrath",
                    "Assets/Item/AsuraItem.png",
                    "Item Description: Pellentesque porta, mauris quis interdum vehicula, urna sapien ultrices velit, nec venenatis dui odio in augue. Cras posuere, enim a cursus convallis, neque turpis malesuada erat, ut adipiscing neque tortor ac erat.",
                    ITEM_CONTENT,
                    sony, adventure, action, xBox360, playStation3);

            var ffIX = new GamePediaDataItem(Guid.NewGuid().ToString(),
                    "Final Fantasy IX",
                    "Assets/Item/ffIX.jpg",
                    "Item Description: Pellentesque porta, mauris quis interdum vehicula, urna sapien ultrices velit, nec venenatis dui odio in augue. Cras posuere, enim a cursus convallis, neque turpis malesuada erat, ut adipiscing neque tortor ac erat.",
                    ITEM_CONTENT,
                    sony, rpg, playStation);

            action.Items.Add(gow3);
            adventure.Items.Add(asura);
            rpg.Items.Add(ffIX);

            this.AllGroups.Add(sony);
            this.AllGroups.Add(microsoft);
            this.AllGroups.Add(genres);
            this.AllGroups.Add(action);
            this.AllGroups.Add(adventure);
            this.AllGroups.Add(rpg);
            this.AllGroups.Add(race);
            this.AllGroups.Add(xBox360);
            this.AllGroups.Add(playStation);
            this.AllGroups.Add(playStation2);
            this.AllGroups.Add(playStation3);
        }
    }
}
