using Godot;

    /// <summary>
    /// A tool, wich merges all debuging functions
    /// </summary>
    class DebugTool
    {
        /// <summary>
        /// Get all child nodes of a given node recursively
        /// </summary>
        /// <param name="node">The parent node</param>
        /// <param name="stage">Is printed infront of each line</param>
        public void getNodesOf(Node node, string stage = "")
        {
            foreach (Node subnode in node.GetChildren())
            {
                GD.Print(stage + subnode + " | " + subnode.Name + " | " + subnode.GetPath());

                getNodesOf(subnode, stage + "-");
            }
        }
    }