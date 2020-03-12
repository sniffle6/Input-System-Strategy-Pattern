using UnityEngine;

namespace _InputTest.Scripts.Input
{
    public interface IMoveInput
    {
        Vector3 MoveDirection { get; }
    }
}