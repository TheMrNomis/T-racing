using UnityEngine;

public class HexCell : MonoBehaviour {

	public HexCoordinates coordinates;

	public Color color;

    public int entree;
    public int sortie;

    public void UpdateColor(Color col)
    {
        this.color = col;
     
        if (color == Color.white)
        {
            entree = -1;
            sortie = -1;
        }
        else if (color == Color.yellow)
        {
            entree = 0;
            sortie = 3;
        }
        else if (color == Color.green)
        {
            entree = 0;
            sortie = 4;
        }
        else if (color == Color.blue)
        {
            entree = 2;
            sortie = 4;
        }
        else if (color == Color.red)
        {
            entree = 1;
            sortie = 4;
        }
        else if (color == Color.black)
        {
            entree = 0;
            sortie = 2;
        }
    }


    public bool isConnectedOut(HexCell adjacentCell) {
        return sortie == adjacentCell.entree;
    }
    public bool isConnectedIn(HexCell adjacentCell)
    {
        return entree == adjacentCell.sortie;
    }

    /// <summary>
    /// Rotate the cell (+45°)
    /// </summary>
    public void rotateCellsC()
    {
        entree++;
        sortie++;
        if (entree >= 6)
            entree = 0;
        if (sortie >= 6)
            entree = 0;

    }
    /// <summary>
    /// Rotate the cell (+45°)
    /// </summary>
    public void rotateCellsCW()
    {
        entree--;
        sortie--;
        if (entree <= 0)
            entree = 5;
        if (sortie <= 0)
            entree = 5;

    }
}