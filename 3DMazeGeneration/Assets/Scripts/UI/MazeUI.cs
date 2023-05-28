using System.Collections;
using System.Collections.Generic;
using DefaultNamespace.MazeGenerators;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class MazeUI : MonoBehaviour
{
    [SerializeField] private Slider widthSlider;
    [SerializeField] private Slider heightSlider;
    [SerializeField] private Slider widthWallSlider;
    [SerializeField] private Slider heightWallSlider;
    [SerializeField] private Slider randomNumberSlider;
    [SerializeField] private Toggle useSeedToggle;

    public void GenerateMaze()
    {
        int width = (int)widthSlider.value;
        int height = (int)heightSlider.value;
        float widthWall = widthWallSlider.value;
        float heightWall = heightWallSlider.value;
        if (useSeedToggle.isOn)
        {
            Random.InitState((int)randomNumberSlider.value);
        }
        else
        {
            Debug.Log("toggled off");
            //If there was a seed given in the first place. The next random seed would always be the same.
            //This is why the system random is used to create a completely random generator again. 
            Random.InitState(new System.Random().Next());
        }

        
        MazeGeneration.ChangeMapSize.Invoke(width,height,widthWall,heightWall);
    }

}
