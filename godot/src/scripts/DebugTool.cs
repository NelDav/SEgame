using Godot;

namespace SEProjekt.src.scripts
{
    class DebugTool
    {
        private void getNodesOf(Node node)
        {
            foreach (Node subnode in node.GetChildren())
            {
                GD.Print(subnode);
                GD.Print(subnode.Name);

                getNodesOf(subnode);
            }
        }
    }
}
