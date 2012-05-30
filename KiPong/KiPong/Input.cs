using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Kinect;

namespace KiPong
{
    public abstract class Input
    {
        /// <summary>
        /// Met à jour l'état des inputs
        /// </summary>
        public virtual void Update() { }

        public virtual void UnloadContent() { }

        /// <summary>
        /// Obtient si le retour est demandé
        /// </summary>
        public abstract bool Back();

        /// <summary>
        /// Obtient si le joueur valide
        /// </summary>
        public abstract bool Valid();

        /// <summary>
        /// Obtient si la pause est demandé
        /// </summary>
        public abstract bool Break();

        /// <summary>
        /// Obtient si l'aide est demandé
        /// </summary>
        public abstract bool Help();
    }
}
