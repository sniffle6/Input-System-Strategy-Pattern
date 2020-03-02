using UnityEngine;

namespace _InputTest.Scripts
{
    public interface IMoveInput
    {
        Vector3 MoveDirection { get; }
    }
}