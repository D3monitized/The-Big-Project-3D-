using UnityEngine; 

public interface IControllable
{
    public void OnPosess();
    public void OnDeposess();
    public void OnMove(Vector3 destination);    
}
