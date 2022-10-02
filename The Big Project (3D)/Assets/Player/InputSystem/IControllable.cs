using UnityEngine; 

public interface IControllable
{
    public void OnPossess();
    public void OnDepossess();
    public void OnWalk(Vector3 position);
}
