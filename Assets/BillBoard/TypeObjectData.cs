using UnityEngine;

public class TypeObjectData
{
    public ItemTypes itemType;
    public GameObject gameObject;

    public override bool Equals(object obj)
    {
        TypeObjectData itemType = obj as TypeObjectData;
        if (itemType == null)
            return false;
        else
            return this.itemType == itemType.itemType;
    }

    public override int GetHashCode()
    {
        return (int) itemType;
    }
}
