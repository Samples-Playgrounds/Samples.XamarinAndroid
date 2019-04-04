// mc++ /*
// mc++  * Copyright 2018 Google LLC.
// mc++  *
// mc++  * Licensed under the Apache License, Version 2.0 (the "License");
// mc++  * you may not use this file except in compliance with the License.
// mc++  * You may obtain a copy of the License at
// mc++  *
// mc++  *      http://www.apache.org/licenses/LICENSE-2.0
// mc++  *
// mc++  * Unless required by applicable law or agreed to in writing, software
// mc++  * distributed under the License is distributed on an "AS IS" BASIS,
// mc++  * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// mc++  * See the License for the specific language governing permissions and
// mc++  * limitations under the License.
// mc++  */
// mc++ package com.google.ar.sceneform.samples.solarsystem;
// mc++ 
// mc++ import android.net.Uri;
// mc++ import android.os.Bundle;
// mc++ import android.support.annotation.NonNull;
// mc++ import android.support.design.widget.Snackbar;
// mc++ import android.support.v7.app.AppCompatActivity;
// mc++ import android.view.GestureDetector;
// mc++ import android.view.MotionEvent;
// mc++ import android.view.View;
// mc++ import android.view.WindowManager;
// mc++ import android.widget.SeekBar;
// mc++ import android.widget.Toast;
// mc++ import com.google.ar.core.Anchor;
// mc++ import com.google.ar.core.Frame;
// mc++ import com.google.ar.core.HitResult;
// mc++ import com.google.ar.core.Plane;
// mc++ import com.google.ar.core.Session;
// mc++ import com.google.ar.core.Trackable;
// mc++ import com.google.ar.core.TrackingState;
// mc++ import com.google.ar.core.exceptions.CameraNotAvailableException;
// mc++ import com.google.ar.core.exceptions.UnavailableException;
// mc++ import com.google.ar.sceneform.AnchorNode;
// mc++ import com.google.ar.sceneform.ArSceneView;
// mc++ import com.google.ar.sceneform.HitTestResult;
// mc++ import com.google.ar.sceneform.Node;
// mc++ import com.google.ar.sceneform.math.Vector3;
// mc++ import com.google.ar.sceneform.rendering.ModelRenderable;
// mc++ import com.google.ar.sceneform.rendering.ViewRenderable;
// mc++ import java.util.concurrent.CompletableFuture;
// mc++ import java.util.concurrent.ExecutionException;
// mc++ 
// mc++ /**
// mc++  * This is a simple example that shows how to create an augmented reality (AR) application using the
// mc++  * ARCore and Sceneform APIs.
// mc++  */
// mc++ public class SolarActivity extends AppCompatActivity {
// mc++   private static final int RC_PERMISSIONS = 0x123;
// mc++   private boolean installRequested;
// mc++ 
// mc++   private GestureDetector gestureDetector;
// mc++   private Snackbar loadingMessageSnackbar = null;
// mc++ 
// mc++   private ArSceneView arSceneView;
// mc++ 
// mc++   private ModelRenderable sunRenderable;
// mc++   private ModelRenderable mercuryRenderable;
// mc++   private ModelRenderable venusRenderable;
// mc++   private ModelRenderable earthRenderable;
// mc++   private ModelRenderable lunaRenderable;
// mc++   private ModelRenderable marsRenderable;
// mc++   private ModelRenderable jupiterRenderable;
// mc++   private ModelRenderable saturnRenderable;
// mc++   private ModelRenderable uranusRenderable;
// mc++   private ModelRenderable neptuneRenderable;
// mc++   private ViewRenderable solarControlsRenderable;
// mc++ 
// mc++   private final SolarSettings solarSettings = new SolarSettings();
// mc++ 
// mc++   // True once scene is loaded
// mc++   private boolean hasFinishedLoading = false;
// mc++ 
// mc++   // True once the scene has been placed.
// mc++   private boolean hasPlacedSolarSystem = false;
// mc++ 
// mc++   // Astronomical units to meters ratio. Used for positioning the planets of the solar system.
// mc++   private static final float AU_TO_METERS = 0.5f;
// mc++ 
// mc++   @Override
// mc++   @SuppressWarnings({"AndroidApiChecker", "FutureReturnValueIgnored"})
// mc++   // CompletableFuture requires api level 24
// mc++   protected void onCreate(Bundle savedInstanceState) {
// mc++     super.onCreate(savedInstanceState);
// mc++ 
// mc++     if (!DemoUtils.checkIsSupportedDeviceOrFinish(this)) {
// mc++       // Not a supported device.
// mc++       return;
// mc++     }
// mc++ 
// mc++     setContentView(R.layout.activity_solar);
// mc++     arSceneView = findViewById(R.id.ar_scene_view);
// mc++ 
// mc++     // Build all the planet models.
// mc++     CompletableFuture<ModelRenderable> sunStage =
// mc++         ModelRenderable.builder().setSource(this, Uri.parse("Sol.sfb")).build();
// mc++     CompletableFuture<ModelRenderable> mercuryStage =
// mc++         ModelRenderable.builder().setSource(this, Uri.parse("Mercury.sfb")).build();
// mc++     CompletableFuture<ModelRenderable> venusStage =
// mc++         ModelRenderable.builder().setSource(this, Uri.parse("Venus.sfb")).build();
// mc++     CompletableFuture<ModelRenderable> earthStage =
// mc++         ModelRenderable.builder().setSource(this, Uri.parse("Earth.sfb")).build();
// mc++     CompletableFuture<ModelRenderable> lunaStage =
// mc++         ModelRenderable.builder().setSource(this, Uri.parse("Luna.sfb")).build();
// mc++     CompletableFuture<ModelRenderable> marsStage =
// mc++         ModelRenderable.builder().setSource(this, Uri.parse("Mars.sfb")).build();
// mc++     CompletableFuture<ModelRenderable> jupiterStage =
// mc++         ModelRenderable.builder().setSource(this, Uri.parse("Jupiter.sfb")).build();
// mc++     CompletableFuture<ModelRenderable> saturnStage =
// mc++         ModelRenderable.builder().setSource(this, Uri.parse("Saturn.sfb")).build();
// mc++     CompletableFuture<ModelRenderable> uranusStage =
// mc++         ModelRenderable.builder().setSource(this, Uri.parse("Uranus.sfb")).build();
// mc++     CompletableFuture<ModelRenderable> neptuneStage =
// mc++         ModelRenderable.builder().setSource(this, Uri.parse("Neptune.sfb")).build();
// mc++ 
// mc++     // Build a renderable from a 2D View.
// mc++     CompletableFuture<ViewRenderable> solarControlsStage =
// mc++         ViewRenderable.builder().setView(this, R.layout.solar_controls).build();
// mc++ 
// mc++     CompletableFuture.allOf(
// mc++             sunStage,
// mc++             mercuryStage,
// mc++             venusStage,
// mc++             earthStage,
// mc++             lunaStage,
// mc++             marsStage,
// mc++             jupiterStage,
// mc++             saturnStage,
// mc++             uranusStage,
// mc++             neptuneStage,
// mc++             solarControlsStage)
// mc++         .handle(
// mc++             (notUsed, throwable) -> {
// mc++               // When you build a Renderable, Sceneform loads its resources in the background while
// mc++               // returning a CompletableFuture. Call handle(), thenAccept(), or check isDone()
// mc++               // before calling get().
// mc++ 
// mc++               if (throwable != null) {
// mc++                 DemoUtils.displayError(this, "Unable to load renderable", throwable);
// mc++                 return null;
// mc++               }
// mc++ 
// mc++               try {
// mc++                 sunRenderable = sunStage.get();
// mc++                 mercuryRenderable = mercuryStage.get();
// mc++                 venusRenderable = venusStage.get();
// mc++                 earthRenderable = earthStage.get();
// mc++                 lunaRenderable = lunaStage.get();
// mc++                 marsRenderable = marsStage.get();
// mc++                 jupiterRenderable = jupiterStage.get();
// mc++                 saturnRenderable = saturnStage.get();
// mc++                 uranusRenderable = uranusStage.get();
// mc++                 neptuneRenderable = neptuneStage.get();
// mc++                 solarControlsRenderable = solarControlsStage.get();
// mc++ 
// mc++                 // Everything finished loading successfully.
// mc++                 hasFinishedLoading = true;
// mc++ 
// mc++               } catch (InterruptedException | ExecutionException ex) {
// mc++                 DemoUtils.displayError(this, "Unable to load renderable", ex);
// mc++               }
// mc++ 
// mc++               return null;
// mc++             });
// mc++ 
// mc++     // Set up a tap gesture detector.
// mc++     gestureDetector =
// mc++         new GestureDetector(
// mc++             this,
// mc++             new GestureDetector.SimpleOnGestureListener() {
// mc++               @Override
// mc++               public boolean onSingleTapUp(MotionEvent e) {
// mc++                 onSingleTap(e);
// mc++                 return true;
// mc++               }
// mc++ 
// mc++               @Override
// mc++               public boolean onDown(MotionEvent e) {
// mc++                 return true;
// mc++               }
// mc++             });
// mc++ 
// mc++     // Set a touch listener on the Scene to listen for taps.
// mc++     arSceneView
// mc++         .getScene()
// mc++         .setOnTouchListener(
// mc++             (HitTestResult hitTestResult, MotionEvent event) -> {
// mc++               // If the solar system hasn't been placed yet, detect a tap and then check to see if
// mc++               // the tap occurred on an ARCore plane to place the solar system.
// mc++               if (!hasPlacedSolarSystem) {
// mc++                 return gestureDetector.onTouchEvent(event);
// mc++               }
// mc++ 
// mc++               // Otherwise return false so that the touch event can propagate to the scene.
// mc++               return false;
// mc++             });
// mc++ 
// mc++     // Set an update listener on the Scene that will hide the loading message once a Plane is
// mc++     // detected.
// mc++     arSceneView
// mc++         .getScene()
// mc++         .addOnUpdateListener(
// mc++             frameTime -> {
// mc++               if (loadingMessageSnackbar == null) {
// mc++                 return;
// mc++               }
// mc++ 
// mc++               Frame frame = arSceneView.getArFrame();
// mc++               if (frame == null) {
// mc++                 return;
// mc++               }
// mc++ 
// mc++               if (frame.getCamera().getTrackingState() != TrackingState.TRACKING) {
// mc++                 return;
// mc++               }
// mc++ 
// mc++               for (Plane plane : frame.getUpdatedTrackables(Plane.class)) {
// mc++                 if (plane.getTrackingState() == TrackingState.TRACKING) {
// mc++                   hideLoadingMessage();
// mc++                 }
// mc++               }
// mc++             });
// mc++ 
// mc++     // Lastly request CAMERA permission which is required by ARCore.
// mc++     DemoUtils.requestCameraPermission(this, RC_PERMISSIONS);
// mc++   }
// mc++ 
// mc++   @Override
// mc++   protected void onResume() {
// mc++     super.onResume();
// mc++     if (arSceneView == null) {
// mc++       return;
// mc++     }
// mc++ 
// mc++     if (arSceneView.getSession() == null) {
// mc++       // If the session wasn't created yet, don't resume rendering.
// mc++       // This can happen if ARCore needs to be updated or permissions are not granted yet.
// mc++       try {
// mc++         Session session = DemoUtils.createArSession(this, installRequested);
// mc++         if (session == null) {
// mc++           installRequested = DemoUtils.hasCameraPermission(this);
// mc++           return;
// mc++         } else {
// mc++           arSceneView.setupSession(session);
// mc++         }
// mc++       } catch (UnavailableException e) {
// mc++         DemoUtils.handleSessionException(this, e);
// mc++       }
// mc++     }
// mc++ 
// mc++     try {
// mc++       arSceneView.resume();
// mc++     } catch (CameraNotAvailableException ex) {
// mc++       DemoUtils.displayError(this, "Unable to get camera", ex);
// mc++       finish();
// mc++       return;
// mc++     }
// mc++ 
// mc++     if (arSceneView.getSession() != null) {
// mc++       showLoadingMessage();
// mc++     }
// mc++   }
// mc++ 
// mc++   @Override
// mc++   public void onPause() {
// mc++     super.onPause();
// mc++     if (arSceneView != null) {
// mc++       arSceneView.pause();
// mc++     }
// mc++   }
// mc++ 
// mc++   @Override
// mc++   public void onDestroy() {
// mc++     super.onDestroy();
// mc++     if (arSceneView != null) {
// mc++       arSceneView.destroy();
// mc++     }
// mc++   }
// mc++ 
// mc++   @Override
// mc++   public void onRequestPermissionsResult(
// mc++       int requestCode, @NonNull String[] permissions, @NonNull int[] results) {
// mc++     if (!DemoUtils.hasCameraPermission(this)) {
// mc++       if (!DemoUtils.shouldShowRequestPermissionRationale(this)) {
// mc++         // Permission denied with checking "Do not ask again".
// mc++         DemoUtils.launchPermissionSettings(this);
// mc++       } else {
// mc++         Toast.makeText(
// mc++                 this, "Camera permission is needed to run this application", Toast.LENGTH_LONG)
// mc++             .show();
// mc++       }
// mc++       finish();
// mc++     }
// mc++   }
// mc++ 
// mc++   @Override
// mc++   public void onWindowFocusChanged(boolean hasFocus) {
// mc++     super.onWindowFocusChanged(hasFocus);
// mc++     if (hasFocus) {
// mc++       // Standard Android full-screen functionality.
// mc++       getWindow()
// mc++           .getDecorView()
// mc++           .setSystemUiVisibility(
// mc++               View.SYSTEM_UI_FLAG_LAYOUT_STABLE
// mc++                   | View.SYSTEM_UI_FLAG_LAYOUT_HIDE_NAVIGATION
// mc++                   | View.SYSTEM_UI_FLAG_LAYOUT_FULLSCREEN
// mc++                   | View.SYSTEM_UI_FLAG_HIDE_NAVIGATION
// mc++                   | View.SYSTEM_UI_FLAG_FULLSCREEN
// mc++                   | View.SYSTEM_UI_FLAG_IMMERSIVE_STICKY);
// mc++       getWindow().addFlags(WindowManager.LayoutParams.FLAG_KEEP_SCREEN_ON);
// mc++     }
// mc++   }
// mc++ 
// mc++   private void onSingleTap(MotionEvent tap) {
// mc++     if (!hasFinishedLoading) {
// mc++       // We can't do anything yet.
// mc++       return;
// mc++     }
// mc++ 
// mc++     Frame frame = arSceneView.getArFrame();
// mc++     if (frame != null) {
// mc++       if (!hasPlacedSolarSystem && tryPlaceSolarSystem(tap, frame)) {
// mc++         hasPlacedSolarSystem = true;
// mc++       }
// mc++     }
// mc++   }
// mc++ 
// mc++   private boolean tryPlaceSolarSystem(MotionEvent tap, Frame frame) {
// mc++     if (tap != null && frame.getCamera().getTrackingState() == TrackingState.TRACKING) {
// mc++       for (HitResult hit : frame.hitTest(tap)) {
// mc++         Trackable trackable = hit.getTrackable();
// mc++         if (trackable instanceof Plane && ((Plane) trackable).isPoseInPolygon(hit.getHitPose())) {
// mc++           // Create the Anchor.
// mc++           Anchor anchor = hit.createAnchor();
// mc++           AnchorNode anchorNode = new AnchorNode(anchor);
// mc++           anchorNode.setParent(arSceneView.getScene());
// mc++           Node solarSystem = createSolarSystem();
// mc++           anchorNode.addChild(solarSystem);
// mc++           return true;
// mc++         }
// mc++       }
// mc++     }
// mc++ 
// mc++     return false;
// mc++   }
// mc++ 
// mc++   private Node createSolarSystem() {
// mc++     Node base = new Node();
// mc++ 
// mc++     Node sun = new Node();
// mc++     sun.setParent(base);
// mc++     sun.setLocalPosition(new Vector3(0.0f, 0.5f, 0.0f));
// mc++ 
// mc++     Node sunVisual = new Node();
// mc++     sunVisual.setParent(sun);
// mc++     sunVisual.setRenderable(sunRenderable);
// mc++     sunVisual.setLocalScale(new Vector3(0.5f, 0.5f, 0.5f));
// mc++ 
// mc++     Node solarControls = new Node();
// mc++     solarControls.setParent(sun);
// mc++     solarControls.setRenderable(solarControlsRenderable);
// mc++     solarControls.setLocalPosition(new Vector3(0.0f, 0.25f, 0.0f));
// mc++ 
// mc++     View solarControlsView = solarControlsRenderable.getView();
// mc++     SeekBar orbitSpeedBar = solarControlsView.findViewById(R.id.orbitSpeedBar);
// mc++     orbitSpeedBar.setProgress((int) (solarSettings.getOrbitSpeedMultiplier() * 10.0f));
// mc++     orbitSpeedBar.setOnSeekBarChangeListener(
// mc++         new SeekBar.OnSeekBarChangeListener() {
// mc++           @Override
// mc++           public void onProgressChanged(SeekBar seekBar, int progress, boolean fromUser) {
// mc++             float ratio = (float) progress / (float) orbitSpeedBar.getMax();
// mc++             solarSettings.setOrbitSpeedMultiplier(ratio * 10.0f);
// mc++           }
// mc++ 
// mc++           @Override
// mc++           public void onStartTrackingTouch(SeekBar seekBar) {}
// mc++ 
// mc++           @Override
// mc++           public void onStopTrackingTouch(SeekBar seekBar) {}
// mc++         });
// mc++ 
// mc++     SeekBar rotationSpeedBar = solarControlsView.findViewById(R.id.rotationSpeedBar);
// mc++     rotationSpeedBar.setProgress((int) (solarSettings.getRotationSpeedMultiplier() * 10.0f));
// mc++     rotationSpeedBar.setOnSeekBarChangeListener(
// mc++         new SeekBar.OnSeekBarChangeListener() {
// mc++           @Override
// mc++           public void onProgressChanged(SeekBar seekBar, int progress, boolean fromUser) {
// mc++             float ratio = (float) progress / (float) rotationSpeedBar.getMax();
// mc++             solarSettings.setRotationSpeedMultiplier(ratio * 10.0f);
// mc++           }
// mc++ 
// mc++           @Override
// mc++           public void onStartTrackingTouch(SeekBar seekBar) {}
// mc++ 
// mc++           @Override
// mc++           public void onStopTrackingTouch(SeekBar seekBar) {}
// mc++         });
// mc++ 
// mc++     // Toggle the solar controls on and off by tapping the sun.
// mc++     sunVisual.setOnTapListener(
// mc++         (hitTestResult, motionEvent) -> solarControls.setEnabled(!solarControls.isEnabled()));
// mc++ 
// mc++     createPlanet("Mercury", sun, 0.4f, 47f, mercuryRenderable, 0.019f, 0.03f);
// mc++ 
// mc++     createPlanet("Venus", sun, 0.7f, 35f, venusRenderable, 0.0475f, 2.64f);
// mc++ 
// mc++     Node earth = createPlanet("Earth", sun, 1.0f, 29f, earthRenderable, 0.05f, 23.4f);
// mc++ 
// mc++     createPlanet("Moon", earth, 0.15f, 100f, lunaRenderable, 0.018f, 6.68f);
// mc++ 
// mc++     createPlanet("Mars", sun, 1.5f, 24f, marsRenderable, 0.0265f, 25.19f);
// mc++ 
// mc++     createPlanet("Jupiter", sun, 2.2f, 13f, jupiterRenderable, 0.16f, 3.13f);
// mc++ 
// mc++     createPlanet("Saturn", sun, 3.5f, 9f, saturnRenderable, 0.1325f, 26.73f);
// mc++ 
// mc++     createPlanet("Uranus", sun, 5.2f, 7f, uranusRenderable, 0.1f, 82.23f);
// mc++ 
// mc++     createPlanet("Neptune", sun, 6.1f, 5f, neptuneRenderable, 0.074f, 28.32f);
// mc++ 
// mc++     return base;
// mc++   }
// mc++ 
// mc++   private Node createPlanet(
// mc++       String name,
// mc++       Node parent,
// mc++       float auFromParent,
// mc++       float orbitDegreesPerSecond,
// mc++       ModelRenderable renderable,
// mc++       float planetScale,
// mc++       float axisTilt) {
// mc++     // Orbit is a rotating node with no renderable positioned at the sun.
// mc++     // The planet is positioned relative to the orbit so that it appears to rotate around the sun.
// mc++     // This is done instead of making the sun rotate so each planet can orbit at its own speed.
// mc++     RotatingNode orbit = new RotatingNode(solarSettings, true, false, 0);
// mc++     orbit.setDegreesPerSecond(orbitDegreesPerSecond);
// mc++     orbit.setParent(parent);
// mc++ 
// mc++     // Create the planet and position it relative to the sun.
// mc++     Planet planet =
// mc++         new Planet(
// mc++             this, name, planetScale, orbitDegreesPerSecond, axisTilt, renderable, solarSettings);
// mc++     planet.setParent(orbit);
// mc++     planet.setLocalPosition(new Vector3(auFromParent * AU_TO_METERS, 0.0f, 0.0f));
// mc++ 
// mc++     return planet;
// mc++   }
// mc++ 
// mc++   private void showLoadingMessage() {
// mc++     if (loadingMessageSnackbar != null && loadingMessageSnackbar.isShownOrQueued()) {
// mc++       return;
// mc++     }
// mc++ 
// mc++     loadingMessageSnackbar =
// mc++         Snackbar.make(
// mc++             SolarActivity.this.findViewById(android.R.id.content),
// mc++             R.string.plane_finding,
// mc++             Snackbar.LENGTH_INDEFINITE);
// mc++     loadingMessageSnackbar.getView().setBackgroundColor(0xbf323232);
// mc++     loadingMessageSnackbar.show();
// mc++   }
// mc++ 
// mc++   private void hideLoadingMessage() {
// mc++     if (loadingMessageSnackbar == null) {
// mc++       return;
// mc++     }
// mc++ 
// mc++     loadingMessageSnackbar.dismiss();
// mc++     loadingMessageSnackbar = null;
// mc++   }
// mc++ }
