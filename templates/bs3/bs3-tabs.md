---
guid: 6e1467cc-4fad-4e3b-98bf-ede66ee7ca75
type: Live
reformat: True
shortenReferences: True
categories: bootstrap 3
scopes: InHtmlLikeFile
parameterOrder: home, tab
home-expression: constant("home")
tab-expression: constant("tab")
---

# bs3-tabs



```
<div role="tabpanel">
    <!-- Nav tabs -->
    <ul class="nav nav-tabs" role="tablist">
        <li role="presentation" class="active">
            <a href="#$home$" aria-controls="$home$" role="tab" data-toggle="tab">$home$</a>
        </li>
        <li role="presentation">
            <a href="#$tab$" aria-controls="$tab$" role="tab" data-toggle="tab">$tab$</a>
        </li>
    </ul>
    <!-- Tab panes -->
    <div class="tab-content">
        <div role="tabpanel" class="tab-pane active" id="$home$">...</div>
        <div role="tabpanel" class="tab-pane" id="$tab$">...</div>
    </div>
</div>
```
