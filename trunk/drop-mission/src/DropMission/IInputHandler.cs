using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace DropMission
{
    public interface IInputHandler
    {
        #region Propiedades

        KeyboardState KeyboardState { get; }

        #endregion
    }
}
