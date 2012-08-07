using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GamePedia.Data;

namespace GamePedia.DataModel
{
    public class GamePediaDataConsole : GamePediaDataGroup
    {
        public GamePediaDataConsole(String uniqueId, String title, String imagePath, String description)
            : base(uniqueId, title, imagePath, description, GroupType.Console)
        {

        }
    }
}
