using UnityEngine;

public class InvItem : MonoBehaviour {
    public virtual string Name { get; set; }

    public virtual string Description { get; set; }

    public virtual Sprite Sprite { get; set; }

    public virtual bool IsUsable { get; set; }

    public virtual bool IsQuestItem { get; set; }

    public virtual void Use() {

    }

    public virtual void Remove() {

    }
}
