# MenuParser
An XML parser for menu items, which also identifies items matching a test string--and all their ancestor nodes.  Conceived as a modified B-tree, in which nodes are unaware of their children, but each node is aware of its complete parent chain.
The XML parsing logic is mostly basic (it supports flexible ordering of sub-nodes within each item), but maintains a stack to track what parent nodes lie above in the heirarchy, and imparts that to each menu-item node created.
This results in a B-tree-like structure which does not support downward traversal, but can be traversed upward very efficiently.  References to child nodes could, however, be added to the parsing routine to produce a doubly-linked tree. 
