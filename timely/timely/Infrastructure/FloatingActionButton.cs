using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace timely.Infrastructure
{
    [TemplatePart(Name = "PART_ContentPath", Type = typeof(Path))]
    public class FloatingActionButton : Control
    {
        private Path _contentPath;

        public static readonly DependencyProperty DataProperty =
            DependencyProperty.Register("Data", typeof(Geometry), typeof(FloatingActionButton), new PropertyMetadata(default(Geometry)));

        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register("Command", typeof(ICommand), typeof(FloatingActionButton), new PropertyMetadata(default(ICommand)));

        static FloatingActionButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FloatingActionButton), new FrameworkPropertyMetadata(typeof(FloatingActionButton)));
        }

        public Geometry Data
        {
            get { return (Geometry)GetValue(DataProperty); }
            set { SetValue(DataProperty, value); }
        }

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _contentPath = Template.FindName("PART_ContentPath", this) as Path;
        }

        protected override void OnMouseEnter(MouseEventArgs e)
        {
            base.OnMouseEnter(e);
            RotateContentPath(0, 360);
        }

        protected override void OnMouseLeave(MouseEventArgs e)
        {
            base.OnMouseLeave(e);
            RotateContentPath(360, 0);
        }

        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonUp(e);
            Command?.Execute(null);
        }

        private void RotateContentPath(double? fromAngle, double? toAngle)
        {
            IEasingFunction easing = new SineEase { EasingMode = EasingMode.EaseIn };

            var rotateAnimation = new DoubleAnimation()
            {
                From = fromAngle,
                To = toAngle,
                EasingFunction = easing,
                Duration = TimeSpan.FromMilliseconds(750)
            };

            Storyboard storyboard = new Storyboard();
            Storyboard.SetTarget(rotateAnimation, _contentPath);
            Storyboard.SetTargetProperty(rotateAnimation, new PropertyPath("(UIElement.RenderTransform).(RotateTransform.Angle)"));
            storyboard.Children.Add(rotateAnimation);
            storyboard.Begin();
        }
    }
}
