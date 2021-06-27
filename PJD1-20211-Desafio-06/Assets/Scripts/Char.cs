public class Char
{
    public float life;
    public bool isAlive;
 
    public float DecrementLife(float bullet)
    {
        
        return life - bullet;
    }

    public float IncrementLife(float newLife)
    {
        return life + newLife;
    }
}
