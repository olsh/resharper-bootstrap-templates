---
guid: ff6794f7-d692-4afb-a2fa-991599053b0b
type: Live
reformat: True
shortenReferences: True
categories: bootstrap 4
scopes: InHtmlLikeFile
parameterOrder: header, lead, content, learn
header-expression: constant("header")
lead-expression: constant("lead")
content-expression: constant("content")
learn-expression: constant("Learn more")
---

# bs4-jumbotron

Jumbotron

```
<div class="jumbotron">
  <h1 class="display-3">$header$</h1>
  <p class="lead">$lead$</p>
  <hr class="m-y-2">
  <p>$content$</p>
  <p class="lead">
    <a class="btn btn-primary btn-lg" href="#" role="button">$learn$</a>
  </p>
</div>
```
