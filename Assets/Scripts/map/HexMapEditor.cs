using UnityEngine;
using UnityEngine.EventSystems;

public class HexMapEditor : MonoBehaviour {

    enum OptionalToggle{
        Ignore, Yes, No
    }

    bool applyColor;
    public Color[] colors;
	public HexGrid hexGrid;
	private Color activeColor;
    OptionalToggle roadMode;
    bool isDrag;
    HexDirection dragDirection;
    HexCell previousCell;

    void Awake () {
		SelectColor(0);
	}

	void Update () {
		if ( Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject() ) {
			HandleInput();
        }else{
            previousCell = null;
        }
    }

	void HandleInput () {
		Ray inputRay = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if (Physics.Raycast(inputRay, out hit)) {
            HexCell currentCell = hexGrid.GetCell(hit.point);
            if (previousCell && previousCell != currentCell){
                ValidateDrag(currentCell);
            } else {
                isDrag = false;
            }
            EditCell(hexGrid.GetCell(hit.point));
            previousCell = currentCell;
        }  else {
            previousCell = null;
        }
    }


    void ValidateDrag(HexCell currentCell){
        for ( dragDirection = HexDirection.NE; dragDirection <= HexDirection.NW; dragDirection++ ) { 
            if (previousCell.GetNeighbor(dragDirection) == currentCell) {
                isDrag = true;
                return;
            }
        }
        isDrag = false;
    }

    void EditCell(HexCell cell){
        if (cell)
        {
            if (applyColor){
                cell.Color = activeColor;
            }
            if (roadMode == OptionalToggle.No) {
                cell.RemoveRoads();
            }
            if (isDrag){
                HexCell otherCell = cell.GetNeighbor(dragDirection.Opposite());
                if (otherCell){
                    if (roadMode == OptionalToggle.Yes) {
                        otherCell.AddRoad(dragDirection);
                    }
                }
            }
        }
    }

    public void SelectColor(int index) {
        applyColor = index >= 0;
        if (applyColor) {
            activeColor = colors[index];
        }
    }

    public void SetRoadMode(int mode){
        roadMode = (OptionalToggle)mode;
    }
}