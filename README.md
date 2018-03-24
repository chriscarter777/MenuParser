# MenuParser
An XML parser for menu items, which also identifies nodes matching a test string--and their parent nodes.  Conceived as a modified B-tree, where nodes are unaware of their children, but each node is aware of its complete parent chain.
The XML parsing logic is mostly basic (it is not opinionated regarding the order of constituent nodes within each item), but maintains a stack to track what parent nodes lie above in the heirarchy, and imparts that to each menu-item node created.
This results in a B-tree-like structure which does not support downward traversal, but can be traversed upward extremely efficiently.  References to child nodes could, however, be added to the parsing routine to produce a doubly-linked tree. 
