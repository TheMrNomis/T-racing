using UnityEngine;

public class HexCell : MonoBehaviour {

	public HexCoordinates coordinates;

    public RectTransform uiRect;

    public HexGridChunk chunk;

    public Color color;

	[SerializeField]
	HexCell[] neighbors;

    [SerializeField]
    bool[] roads;

    public bool HasRoadThroughEdge(HexDirection direction){
        return roads[(int)direction];
    }

    public bool HasRoads{
        get{
            for (int i = 0; i < roads.Length; i++){
                if (roads[i]){
                    return true;
                }
            }
            return false;
        }
    }

    public void AddRoad(HexDirection direction){
        if (!roads[(int)direction]){
            SetRoad((int)direction, true);
        }
    }

    public void RemoveRoads(){
        for (int i = 0; i < neighbors.Length; i++){
            if (roads[i]){
                SetRoad(i, false);
            }
        }
    }

    void SetRoad(int index, bool state){
        roads[index] = state;
        neighbors[index].roads[(int)((HexDirection)index).Opposite()] = state;
        neighbors[index].RefreshSelfOnly();
        RefreshSelfOnly();
    }

    public HexCell GetNeighbor (HexDirection direction) {
		return neighbors[(int)direction];
	}

	public void SetNeighbor (HexDirection direction, HexCell cell) {
		neighbors[(int)direction] = cell;
		cell.neighbors[(int)direction.Opposite()] = this;
	}

    void RefreshSelfOnly()
    {
        chunk.Refresh();
    }

    public Color Color {
        get{ return color;
        } set {
            if (color == value) { return; }
            color = value;
            Refresh();
        }
    }
    
    void Refresh() {
        if (chunk) {
            chunk.Refresh();
            for (int i = 0; i < neighbors.Length; i++) {
                HexCell neighbor = neighbors[i];
                if (neighbor != null && neighbor.chunk != chunk) {
                    neighbor.chunk.Refresh();
                }
            }
        }
    }

   
}