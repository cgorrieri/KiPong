using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KiPong
{
    /// <summary>
    /// Elément du jeu
    /// </summary>
    public abstract class GameObject
    {
        /// <summary>
        /// Le jeu à qui appertient cet objet
        /// </summary>
        protected KiPongGame game;

        /// <summary>
        /// Construit un objet de jeu
        /// </summary>
        /// <param name="g">Le jeu à qui appertient cet objet</param>
        public GameObject(KiPongGame g)
        {
            game = g;
        }

        /// <summary>
        /// Méthode pour mettre à jour l'objet
        /// </summary>
        public abstract void Update();

        /// <summary>
        /// Méthode pour dessiner l'objet
        /// </summary>
        public abstract void Draw();
    }
}
