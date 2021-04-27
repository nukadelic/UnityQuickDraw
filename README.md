# Unity Quick Draw 

* Draw 2D anti-aliased, GPU-instance-supported, 0 allocation shapes ( circle,line,arrow ) with one line of code in Unity
* Draw Labels ( WIP ) 
* Draw 3D Shapes ( WIP ) 

![](https://i.imgur.com/vaSg7r9.png)

# Installation 

Edit the `manifest.json` file located in the `Packages` folder of your unity project and
add the follwing line to the list of `dependencies`:
```json
"com.nukadelic.unityquickdraw": "https://github.com/nukadelic/UnityQuickDraw.git"
```

Draw can be noramally called within the update function:
```cs
...
void Update()
{
	if( drawCircle ) Draw.circle( points[ i ] , 0.05f , Color.white, Color.black, 0.2f );

	if( drawPoint ) Draw.point( points[ i ], Color.yellow );

	if( drawLabels ) Draw.label( points[ i ], i.ToString(), Color.white );

	if (i > 0 && drawRods) Draw.rod(points[i], points[ i - 1 ], Color.red);

	if (i > 0 && drawLines) Draw.line(points[i], points[ i - 1 ], Color.red);
}
```

If you want to draw both in play mode and in editor ( instead of using Gizmos ) , implement MonoDraw and override the `OnDraw()` method: 
```cs
public class ScriptName : MonoDraw
{
	public override void OnDraw()
	{
		Draw.Point( ... );
	}
}
```

WIP : Forked from Miguel Ferreira 
