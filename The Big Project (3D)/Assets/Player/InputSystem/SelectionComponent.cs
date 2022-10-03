using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic; 


public class SelectionComponent : MonoBehaviour
{
	[SerializeField]
    private GameObject SelectionBoxPrefab;
	
    private Camera Cam; 
	private GameObject SelectionBox; 
	private bool IsActive = false; 

	private Vector2 MouseStartPosition;
	private Bounds bounds; 

	private void Awake()
	{
		Cam = Camera.main; 
	}

	public RaycastHit QuickSelect()
	{
		RaycastHit hit = new RaycastHit();
		GetMouseInteraction(ref hit);

		return hit;
	}

	private void GetMouseInteraction(ref RaycastHit hit)
	{
		Vector2 mousePos = Mouse.current.position.ReadValue();
		Ray ray = Camera.main.ScreenPointToRay(mousePos);
		Physics.Raycast(ray, out hit);		
	}

	#region SelectionBox
	private void Update()
	{
		if (IsActive && SelectionBox != null)
			ResizeSelectionBox(); 
	}

	public void ActivateSelectionBox()
	{
		CreateSelectionBox();
		IsActive = true; 
	}

	public List<IControllable> DeactivateSelectionBox()
	{
		IsActive = false;
		return DestroySelectionBox(); 
	}

	private void CreateSelectionBox() //Mouse clicked
	{
		SelectionBox = Instantiate(SelectionBoxPrefab);
		SelectionBox.transform.SetParent(GameObject.Find("Canvas_Select").transform); 
		SelectionBox.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
		MouseStartPosition = Mouse.current.position.ReadValue(); 
	}

	private void ResizeSelectionBox() //Mouse held
	{
		float width = Mouse.current.position.x.ReadValue() - MouseStartPosition.x;
		float height = Mouse.current.position.y.ReadValue() - MouseStartPosition.y;

		//Center of box
		SelectionBox.GetComponent<RectTransform>().anchoredPosition = MouseStartPosition + new Vector2(width / 2, height / 2);
		//Size of box
		SelectionBox.GetComponent<RectTransform>().sizeDelta = new Vector2(Mathf.Abs(width), Mathf.Abs(height));

		bounds = new Bounds(SelectionBox.GetComponent<RectTransform>().anchoredPosition, SelectionBox.GetComponent<RectTransform>().sizeDelta);				
	}

	private List<IControllable> DestroySelectionBox() //Mouse released
	{
		List<IControllable> pawns = new List<IControllable>();
		
		foreach(GameObject go in GameManager.Instance.PartyMembers)
		{
			Vector2 screenPos = Cam.WorldToScreenPoint(go.transform.position);
			if (bounds.Contains(screenPos))
				pawns.Add(go.GetComponent<IControllable>()); 
		}
		
		Destroy(SelectionBox); 
		return pawns;

	}
	#endregion
}
