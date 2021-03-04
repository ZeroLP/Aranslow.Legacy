using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.IO;

namespace Aranslow.Objects
{
    /// <summary>
    /// Object base class
    /// </summary>
    internal class GameObject
    {
        internal GameObject(ModelType mType, Point spawnPosition, ActionState aState = ActionState.Idle)
        {
            CurrentModelType = mType;
            CurrentActionState = aState;

            if (spawnPosition != null)
                WorldPosition = spawnPosition;

            ModelSprite = new Sprite(new BitmapImage(
                          new Uri(Directory.GetCurrentDirectory() + $@"\Assets\{Enum.GetName(typeof(ModelType), CurrentModelType)}_{Enum.GetName(typeof(ActionState), CurrentActionState)}.png")));
            ModelSprite.FrameDuration = 100;

            Model.Children.Add(ModelSprite.Image);
            ModelSprite.StartAnimation();

            BoundingBox.Margin = new Thickness(WorldPosition.X - 5, WorldPosition.Y - 5, 0, 0);
            BoundingBox.Width = ModelSprite.FrameWidth;
            BoundingBox.Height = ModelSprite.FrameWidth;
        }

        public Grid Model = new Grid() { HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Top };
        public Sprite ModelSprite;
        public ModelType CurrentModelType;
        public int Health;
        public int Mana;

        public Point WorldPosition
        {
            get { return new Point(Model.Margin.Left, Model.Margin.Top); }
            set { Model.Margin = new Thickness(value.X, value.Y, 0, 0); }
        }

        public Border BoundingBox = new Border() { HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Top, BorderBrush = System.Windows.Media.Brushes.LightGreen, 
                                                   BorderThickness = new Thickness(1, 1, 1, 1)};

        public int Velocity = 5;
        public ActionState CurrentActionState = ActionState.Idle;
        private ActionState localCachedActionState;

        public virtual void Draw()
        {
            if (localCachedActionState != CurrentActionState)
            {
                ModelSprite.PauseAnimation();

                ModelSprite = new Sprite(new BitmapImage(
                new Uri(Directory.GetCurrentDirectory() + $@"\Assets\{Enum.GetName(typeof(ModelType), CurrentModelType)}_{Enum.GetName(typeof(ActionState), CurrentActionState)}.png")));

                ModelSprite.StartAnimation();

                localCachedActionState = CurrentActionState;
            }

            Model.Children.RemoveAt(0);
            Model.Children.Add(ModelSprite.Image);

            BoundingBox.Margin = new Thickness(WorldPosition.X - 5, WorldPosition.Y - 5, 0, 0);
        }

        public virtual void Act(ActionState aState)
        {
            ModelSprite = new Sprite(new BitmapImage(
            new Uri(Directory.GetCurrentDirectory() + $@"\Assets\{Enum.GetName(typeof(ModelType), CurrentModelType)}_{Enum.GetName(typeof(ActionState), aState)}.png")));

            ModelSprite.StartAnimation();

            CurrentActionState = aState;
        }

        public virtual void Move()
        {
            if (CurrentActionState == ActionState.WalkRight)
                WorldPosition = new Point(WorldPosition.X + Velocity, WorldPosition.Y);
            else if (CurrentActionState == ActionState.WalkLeft)
                WorldPosition = new Point(WorldPosition.X - Velocity, WorldPosition.Y);
        }

        public enum ModelType
        {
            Player
        }

        public enum ActionState
        {
            Idle,
            WalkRight,
            WalkLeft,
            Jump,
            BasicAttack
        }
    }
}
