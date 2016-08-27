---
guid: 79fec4ce-fc27-466d-a76d-0ece370f8279
type: Live
reformat: True
shortenReferences: True
categories: bootstrap 3
scopes: InHtmlLikeFile
parameterOrder: inverse, title, home, link
inverse-expression: constant("navbar-inverse")
title-expression: constant("Title")
home-expression: constant("Home")
link-expression: constant("Link")
---

# bs3-navbar-basic

Navbar basic

```
<div class="navbar $inverse$">
    <div class="container-fluid">
        <a class="navbar-brand" href="#">$title$</a>
        <ul class="nav navbar-nav">
            <li class="active">
                <a href="#">$home$</a>
            </li>
            <li>
                <a href="#">$link$</a>
            </li>
        </ul>
    </div>
</div>
```
