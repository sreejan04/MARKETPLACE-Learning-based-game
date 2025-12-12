using UnityEngine;
using UnityEngine.SceneManagement;
public class Mainmenu : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void starting()
    {
        SceneManager.LoadScene("Demonstration");
    }
    public void chooseavatar()
    {
        SceneManager.LoadScene("characteroption");
    }
    public void storyselect()
    {
        SceneManager.LoadScene("storyselection");
    }
    public void settings()
    {
        SceneManager.LoadScene("settings");
    }
    public void exit()
    {
        Application.Quit(); 
    }
}
