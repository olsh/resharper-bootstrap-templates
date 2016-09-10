---
guid: 01e2bb30-b26e-4e63-862c-ce35e14f2e11
type: Live
reformat: True
shortenReferences: True
categories: bootstrap 4
scopes: InHtmlLikeFile
parameterOrder: title, text, link, src
title-expression: constant("Card title")
text-expression: constant("Card text")
link-expression: constant("Go somewhere")
src-expression: constant("")
---

# bs4-card

Card

```
<div class="card">
  <img class="card-img-top" src="$src$" alt="Card image cap">
  <div class="card-block">
    <h4 class="card-title">$title$</h4>
    <p class="card-text">$text$</p>
    <a href="#" class="btn btn-primary">$link$</a>
  </div>
</div>
```
