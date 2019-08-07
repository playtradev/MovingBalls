using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{

	public static GameManagerScript Instance;

	Dictionary<SphereType, SizeType> CurrentSpheres = new Dictionary<SphereType, SizeType>();




	public SphereScript CurrentSphere;

    public int maxMoves;
    public int CurrentMoves;

    public TextMeshProUGUI Text;

    private void Awake()
    {
        Instance = this;
		CurrentSpheres.Add(SphereType.Red, SizeType.Small);
		CurrentSpheres.Add(SphereType.Blue, SizeType.Small);
		CurrentSpheres.Add(SphereType.Green, SizeType.Small);
		CurrentSpheres.Add(SphereType.Yellow, SizeType.Small);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Text.text = CurrentMoves + "/" + maxMoves;


		if(CurrentMoves == maxMoves)
		{
			Invoke("reloadScene", 3);
		}
    }

    private void reloadScene()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}


	public void UpdateState(SphereType st, SizeType sit)
	{
		CurrentSpheres[st] = sit;

		if (CurrentSpheres.Where(r => r.Value == SizeType.Large).ToList().Count == 4)
        {
			Invoke("reloadScene", 3);
        }
	}
}
