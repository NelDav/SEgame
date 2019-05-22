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
        public void getNodesOf(Node node)
        {
            foreach (Node subnode in node.GetChildren())
            {
                GD.Print(subnode);
                GD.Print(subnode.Name);

                getNodesOf(subnode);
            }
        }
    }