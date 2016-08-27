---
guid: ef6f0d44-2706-427d-b2e4-c4eeb24ec3f6
type: Live
reformat: True
shortenReferences: True
categories: bootstrap 3
scopes: InHtmlLikeFile
parameterOrder: position, type, submit
position-expression: list("right,left")
type-expression: list("default,primary,success,warning,danger")
submit-expression: constant("Submit")
---

# bs3-navbar-form

Navbar form

```
<form class="navbar-form pull-$position$">
    <input type="text" style="width: 200px;">
    <button type="submit" class="btn btn-$type$">$submit$</button>
</form>
```
