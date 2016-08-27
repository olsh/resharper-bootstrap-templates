---
guid: f2a78619-513e-49f6-a44a-a919d70c263d
type: Live
reformat: True
shortenReferences: True
categories: bootstrap 3
scopes: InHtmlLikeFile
parameterOrder: position, href, title, home, link
position-expression: list("bottom,top")
href-expression: constant("#")
title-expression: constant("Title")
home-expression: constant("Home")
link-expression: constant("Link")
---

# bs3-navbar-fixed

Navbar fixed

```
<nav class="navbar navbar-default navbar-fixed-$position$" role="navigation">
    <div class="container">
        <a class="navbar-brand" href="$href$">$title$</a>
        <ul class="nav navbar-nav">
            <li class="active">
                <a href="#">$home$</a>
            </li>
            <li>
                <a href="#">$link$</a>
            </li>
        </ul>
    </div>
</nav>
```
