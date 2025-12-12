using UnityEngine;
using UnityEngine.SceneManagement;
public class settings : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void onexit()
    {
        SceneManager.LoadScene("Mainmenu");
    }
    public void onreturn()
    {
        SceneManager.LoadScene("Demonstration");
    }
}
