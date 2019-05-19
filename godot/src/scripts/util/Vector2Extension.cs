using Godot;

namespace CustomExtensions
{
    public static class Vector2Extension
    {
        public static Vector2 Scale(this Vector2 vector2, float factor)
        {
            vector2.x *= factor;
            vector2.y *= factor;
            return vector2;
        }
    }
}