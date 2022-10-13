using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System.Collections.Generic; 

public class MouseUIComponent : MonoBehaviour
{
	public static MouseUIComponent Instance; 

	[SerializeField]
	private Sprite PanSprite;
	[SerializeField]
	private Sprite DefaultSprite;
	[SerializeField]
	private Sprite AttackSprite;
	[SerializeField]
	private GameObject TextPrefab;

	private Image CursorImage;
	private Text CursorText; 
	private bool followMouse = true;

	[SerializeField]
	private GameObject SelectionBoxPrefab;

	private GameObject SelectionBox;
	private bool IsActive = false;

	private Vector2 MouseStartPosition;
	private Bounds bounds;

	#region MouseUIPublic
	public void ChangeCursorToPanSprite()
	{
		CursorImage.sprite = PanSprite;		
		followMouse = false; 
	}

	public void ChangeCursorToDefaultSprite()
	{
		CursorImage.sprite = DefaultSprite; 
		followMouse = true; 
	}

	public void ChangeCursorToAttackSprite()
	{
		CursorImage.sprite = AttackSprite;
		followMouse = true; 
	}

	public void DeactivateMovementCost() => CursorText.gameObject.SetActive(false); 

	public void DisplayMoveCost()
	{
		Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue()); 
		RaycastHit hit; 

		if(Physics.Raycast(ray, out hit))
		{
			if(hit.collider.GetComponent<CombatantBase>() as EnemyCharacter)
			{
				//Update icon to attack + move cost
				ChangeCursorToAttackSprite();

				int cost;

				if (GetDistanceToTarget(hit) > CombatManager.Instance.CurrentCombatant.Stats.GetAttackRange())
					cost = GetMoveCost(hit) + CombatManager.Instance.CurrentCombatant.Stats.GetMeleeAttackCost();
				else
					cost = CombatManager.Instance.CurrentCombatant.Stats.GetMeleeAttackCost(); 

				CursorText.text = cost.ToString();
				CursorText.color = cost <= CombatManager.Instance.CurrentCombatant.CurrentActionPoints ? Color.green : Color.red;
				CursorText.gameObject.SetActive(true);

			}
			else if(hit.collider.GetComponent<CombatantBase>() as PlayerCharacter)
			{
				//Update icon to default
				ChangeCursorToDefaultSprite();
				CursorText.gameObject.SetActive(false); 
			}
			else
			{
				//Display default icon + move cost
				ChangeCursorToDefaultSprite();
				int cost = GetMoveCost(hit); 
				CursorText.text = cost.ToString();
				CursorText.color = cost <= CombatManager.Instance.CurrentCombatant.CurrentActionPoints ? Color.green : Color.red;
				CursorText.gameObject.SetActive(true);
			}
		}
		
	}
	#endregion

	#region SelectionBoxPublic
	public RaycastHit QuickSelect()
	{
		RaycastHit hit = new RaycastHit();
		GetMouseInteraction(ref hit);

		return hit;
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

	#endregion

	#region MouseUIPrivate

	private int GetMoveCost(RaycastHit hit)
	{
		float distance = GetDistanceToTarget(hit); 	
		return Mathf.RoundToInt(distance / CombatManager.Instance.CurrentCombatant.Stats.GetMoveCostPerDistance());		
	}

	private float GetDistanceToTarget(RaycastHit hit)
	{
		Vector3 combatantPos = CombatManager.Instance.CurrentCombatant.transform.position;
		return Vector3.Distance(combatantPos, hit.point);
	}

	private void Awake()
	{
		if (Instance != null && Instance != this)
			Destroy(this);
		else
			Instance = this; 
	}

	private void Update()
	{
		if (followMouse)
		{
			CursorImage.transform.position = Mouse.current.position.ReadValue();
			CursorText.transform.position = Mouse.current.position.ReadValue() + new Vector2(20, -20);
		}
		else
			CursorImage.transform.position = new Vector2(Screen.width / 2, Screen.height / 2);

		if(GameManager.Instance.CurrentGameState == GameManager.GameState.Combatmode)
			DisplayMoveCost();

		//Selection Component
		if (IsActive && SelectionBox != null)
			ResizeSelectionBox();
	}

	#endregion

	#region SelectionBoxPrivate

	private void GetMouseInteraction(ref RaycastHit hit)
	{
		Vector2 mousePos = Mouse.current.position.ReadValue();
		Ray ray = Camera.main.ScreenPointToRay(mousePos);
		Physics.Raycast(ray, out hit);
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

		foreach (GameObject go in GameManager.Instance.PartyMembers)
		{
			Vector2 screenPos = Camera.main.WorldToScreenPoint(go.transform.position);
			if (bounds.Contains(screenPos))
				pawns.Add(go.GetComponent<IControllable>());
		}

		Destroy(SelectionBox);
		return pawns;

	}

	#endregion

	private void OnEnable()
	{
		CursorImage = new GameObject().AddComponent<Image>();
		CursorImage.raycastTarget = false; 
		CursorImage.transform.SetParent(GameObject.Find("Canvas_Select").transform);
		CursorImage.sprite = DefaultSprite; 
		Cursor.visible = false;

		CursorText = Instantiate(TextPrefab).GetComponent<Text>();
		CursorText.transform.SetParent(GameObject.Find("Canvas_Select").transform); 	
	}

	private void OnDisable()
	{
		Destroy(CursorImage);
	}
}
