---
guid: 524abc98-2d7b-415e-ad91-353ea1f430b9
type: Live
reformat: True
shortenReferences: True
categories: bootstrap 3
scopes: InHtmlLikeFile
parameterOrder: type, title, text
type-expression: list("danger,info,primary,success,warning,default")
title-expression: constant("title")
text-expression: constant("text")
---

# bs3-panel



```
<div class="panel panel-$type$">
    <div class="panel-heading">
        <h3 class="panel-title">$title$</h3>
    </div>
    <div class="panel-body">
        $text$
    </div>
</div>
```
