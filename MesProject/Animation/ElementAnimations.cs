using System.Windows;
using System.Windows.Media.Animation;

namespace MesProject.Animation
{
    public static class ElementAnimations
    {
        public static void FadeInAndSlideFromBottom(this FrameworkElement element, float seconds)
        {
            var storyboard = new Storyboard();
            storyboard.AddFadeIn(seconds);
            storyboard.AddSlideFromBottom(seconds, element.ActualHeight);
            storyboard.Begin(element);
        }
    }
}
