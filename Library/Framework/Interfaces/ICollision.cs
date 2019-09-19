using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FreedomFodgeFrameWork.Interfaces
{
    public interface ICollision
    {
        bool BoundaryWallCollision(Game game);
        bool PlayerEnemyCollision(Game game);
        bool FireCollision(Game game,PictureBox pictureBox);
    }
}
