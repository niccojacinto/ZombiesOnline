using UnityEngine;
using System.Collections;

public interface ICharacter {
    void MoveForward(float _scale);
    void MoveRight(float _scale);
    void RotateRight(float _scale);
}

public interface ISoldier {
    void Shoot();
    void MoveForward(float _scale);
    void MoveRight(float _scale);
    void RotateRight(float _scale);
    bool DropItemAtIndex(int _index);
    bool UseItemAtIndex(int _index);
    void Equip(Gun _gun);
}

public interface IGun {
    void Shoot();
}

public interface IInventory {
    bool AddItem(Item _item);
    bool DropItemAtIndex(int _index);
    bool UseItemAtIndex(int _index);
}

public interface IDamageable {
    void CmdTakeDamage();
    void CmdKill();
}