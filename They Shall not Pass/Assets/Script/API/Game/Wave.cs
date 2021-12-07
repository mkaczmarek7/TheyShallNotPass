using API.Dinosaur;

[System.Serializable]
public class Wave
{
    public DinosaurScriptableObject[] dinosaurControllers;
    public float[] spawnInterval;
    public int[] maxEnemies;
}
