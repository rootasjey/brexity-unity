using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInvItem {

	string Name { get; set; }

    Sprite Image { get; set; }

    void OnPickup();

    void Use();

    void Drop();
}
