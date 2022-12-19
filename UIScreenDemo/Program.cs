using UILibrary.Scenes;

namespace UIScreenDemo
{
    internal class Program
    {
        static void Main()
        {
            _ = new SceneManager(new(1280, 720));

            ScreenOne screenOne = new();
            ScreenTwo screenTwo = new();

            SceneManager.Instance.AddScene("s1", screenOne);
            SceneManager.Instance.SetScene("s1");
            SceneManager.Instance.AddScene("s2", screenTwo);
            SceneManager.Instance.Run();
        }
    }
}