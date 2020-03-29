using UnityEngine;

namespace _InputTest.Entity.Scripts.Input
{
    public interface IMoveInput
    {
        Vector3 MoveDirection { get; }
    }
}