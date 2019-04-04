// mc++ /*
// mc++ Copyright 2018 Google LLC
// mc++ 
// mc++ Licensed under the Apache License, Version 2.0 (the "License");
// mc++ you may not use this file except in compliance with the License.
// mc++ You may obtain a copy of the License at
// mc++ 
// mc++     https://www.apache.org/licenses/LICENSE-2.0
// mc++ 
// mc++ Unless required by applicable law or agreed to in writing, software
// mc++ distributed under the License is distributed on an "AS IS" BASIS,
// mc++ WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// mc++ See the License for the specific language governing permissions and
// mc++ limitations under the License.
// mc++ */
// mc++ package com.google.devrel.ar.codelab;
// mc++ 
// mc++ import android.content.Intent;
// mc++ import android.graphics.Bitmap;
// mc++ import android.graphics.Point;
// mc++ import android.net.Uri;
// mc++ import android.os.Bundle;
// mc++ import android.os.Environment;
// mc++ import android.os.Handler;
// mc++ import android.os.HandlerThread;
// mc++ import android.os.Looper;
// mc++ import android.support.design.widget.FloatingActionButton;
// mc++ import android.support.design.widget.Snackbar;
// mc++ import android.support.v4.content.FileProvider;
// mc++ import android.support.v7.app.AlertDialog;
// mc++ import android.support.v7.app.AppCompatActivity;
// mc++ import android.support.v7.widget.Toolbar;
// mc++ import android.view.PixelCopy;
// mc++ import android.view.SurfaceHolder;
// mc++ import android.view.View;
// mc++ import android.view.Menu;
// mc++ import android.view.MenuItem;
// mc++ import android.widget.ImageView;
// mc++ import android.widget.LinearLayout;
// mc++ import android.widget.Toast;
// mc++ 
// mc++ import com.google.ar.core.Anchor;
// mc++ import com.google.ar.core.Frame;
// mc++ import com.google.ar.core.HitResult;
// mc++ import com.google.ar.core.Plane;
// mc++ import com.google.ar.core.Trackable;
// mc++ import com.google.ar.core.TrackingState;
// mc++ import com.google.ar.sceneform.AnchorNode;
// mc++ import com.google.ar.sceneform.ArSceneView;
// mc++ import com.google.ar.sceneform.Scene;
// mc++ import com.google.ar.sceneform.rendering.ModelRenderable;
// mc++ import com.google.ar.sceneform.rendering.Renderable;
// mc++ import com.google.ar.sceneform.rendering.Renderer;
// mc++ import com.google.ar.sceneform.ux.ArFragment;
// mc++ import com.google.ar.sceneform.ux.TransformableNode;
// mc++ 
// mc++ import java.io.ByteArrayOutputStream;
// mc++ import java.io.File;
// mc++ import java.io.FileOutputStream;
// mc++ import java.io.IOException;
// mc++ import java.nio.ByteBuffer;
// mc++ import java.text.SimpleDateFormat;
// mc++ import java.util.Date;
// mc++ import java.util.List;
// mc++ import java.util.concurrent.CompletableFuture;
// mc++ 
// mc++ public class MainActivity extends AppCompatActivity {
// mc++ 
// mc++     private ArFragment fragment;
// mc++     private PointerDrawable pointer = new PointerDrawable();
// mc++     private boolean isTracking;
// mc++     private boolean isHitting;
// mc++ 
// mc++     @Override
// mc++     protected void onCreate(Bundle savedInstanceState) {
// mc++         super.onCreate(savedInstanceState);
// mc++         setContentView(R.layout.activity_main);
// mc++         Toolbar toolbar = (Toolbar) findViewById(R.id.toolbar);
// mc++         setSupportActionBar(toolbar);
// mc++ 
// mc++         FloatingActionButton fab = (FloatingActionButton) findViewById(R.id.fab);
// mc++         fab.setOnClickListener(new View.OnClickListener() {
// mc++             @Override
// mc++             public void onClick(View view) {
// mc++                 takePhoto();
// mc++             }
// mc++         });
// mc++         fragment = (ArFragment)
// mc++                 getSupportFragmentManager().findFragmentById(R.id.sceneform_fragment);
// mc++         fragment.getArSceneView().getScene().setOnUpdateListener(frameTime -> {
// mc++             fragment.onUpdate(frameTime);
// mc++             onUpdate();
// mc++         });
// mc++ 
// mc++         initializeGallery();
// mc++     }
// mc++ 
// mc++     private String generateFilename() {
// mc++         String date =
// mc++                 new SimpleDateFormat("yyyyMMddHHmmss", java.util.Locale.getDefault()).format(new Date());
// mc++         return Environment.getExternalStoragePublicDirectory(
// mc++                 Environment.DIRECTORY_PICTURES) + File.separator + "Sceneform/" + date + "_screenshot.jpg";
// mc++     }
// mc++ 
// mc++     private void saveBitmapToDisk(Bitmap bitmap, String filename) throws IOException {
// mc++ 
// mc++         File out = new File(filename);
// mc++         if (!out.getParentFile().exists()) {
// mc++             out.getParentFile().mkdirs();
// mc++         }
// mc++         try (FileOutputStream outputStream = new FileOutputStream(filename);
// mc++              ByteArrayOutputStream outputData = new ByteArrayOutputStream()) {
// mc++             bitmap.compress(Bitmap.CompressFormat.PNG, 100, outputData);
// mc++             outputData.writeTo(outputStream);
// mc++             outputStream.flush();
// mc++             outputStream.close();
// mc++         } catch (IOException ex) {
// mc++             throw new IOException("Failed to save bitmap to disk", ex);
// mc++         }
// mc++     }
// mc++ 
// mc++     private void takePhoto() {
// mc++         final String filename = generateFilename();
// mc++         ArSceneView view = fragment.getArSceneView();
// mc++ 
// mc++         // Create a bitmap the size of the scene view.
// mc++         final Bitmap bitmap = Bitmap.createBitmap(view.getWidth(), view.getHeight(),
// mc++                 Bitmap.Config.ARGB_8888);
// mc++ 
// mc++         // Create a handler thread to offload the processing of the image.
// mc++         final HandlerThread handlerThread = new HandlerThread("PixelCopier");
// mc++         handlerThread.start();
// mc++         // Make the request to copy.
// mc++         PixelCopy.request(view, bitmap, (copyResult) -> {
// mc++             if (copyResult == PixelCopy.SUCCESS) {
// mc++                 try {
// mc++                     saveBitmapToDisk(bitmap, filename);
// mc++                 } catch (IOException e) {
// mc++                     Toast toast = Toast.makeText(MainActivity.this, e.toString(),
// mc++                             Toast.LENGTH_LONG);
// mc++                     toast.show();
// mc++                     return;
// mc++                 }
// mc++                 Snackbar snackbar = Snackbar.make(findViewById(android.R.id.content),
// mc++                         "Photo saved", Snackbar.LENGTH_LONG);
// mc++                 snackbar.setAction("Open in Photos", v -> {
// mc++                     File photoFile = new File(filename);
// mc++ 
// mc++                     Uri photoURI = FileProvider.getUriForFile(MainActivity.this,
// mc++                             MainActivity.this.getPackageName() + ".ar.codelab.name.provider",
// mc++                             photoFile);
// mc++                     Intent intent = new Intent(Intent.ACTION_VIEW, photoURI);
// mc++                     intent.setDataAndType(photoURI, "image/*");
// mc++                     intent.addFlags(Intent.FLAG_GRANT_READ_URI_PERMISSION);
// mc++                     startActivity(intent);
// mc++ 
// mc++                 });
// mc++                 snackbar.show();
// mc++             } else {
// mc++                 Toast toast = Toast.makeText(MainActivity.this,
// mc++                         "Failed to copyPixels: " + copyResult, Toast.LENGTH_LONG);
// mc++                 toast.show();
// mc++             }
// mc++             handlerThread.quitSafely();
// mc++         }, new Handler(handlerThread.getLooper()));
// mc++     }
// mc++ 
// mc++     private void onUpdate() {
// mc++         boolean trackingChanged = updateTracking();
// mc++         View contentView = findViewById(android.R.id.content);
// mc++         if (trackingChanged) {
// mc++             if (isTracking) {
// mc++                 contentView.getOverlay().add(pointer);
// mc++             } else {
// mc++                 contentView.getOverlay().remove(pointer);
// mc++             }
// mc++             contentView.invalidate();
// mc++         }
// mc++ 
// mc++         if (isTracking) {
// mc++             boolean hitTestChanged = updateHitTest();
// mc++             if (hitTestChanged) {
// mc++                 pointer.setEnabled(isHitting);
// mc++                 contentView.invalidate();
// mc++             }
// mc++         }
// mc++     }
// mc++     private boolean updateTracking() {
// mc++         Frame frame = fragment.getArSceneView().getArFrame();
// mc++         boolean wasTracking = isTracking;
// mc++         isTracking = frame.getCamera().getTrackingState() == TrackingState.TRACKING;
// mc++         return isTracking != wasTracking;
// mc++     }
// mc++     private boolean updateHitTest() {
// mc++         Frame frame = fragment.getArSceneView().getArFrame();
// mc++         android.graphics.Point pt = getScreenCenter();
// mc++         List<HitResult> hits;
// mc++         boolean wasHitting = isHitting;
// mc++         isHitting = false;
// mc++         if (frame != null) {
// mc++             hits = frame.hitTest(pt.x, pt.y);
// mc++             for (HitResult hit : hits) {
// mc++                 Trackable trackable = hit.getTrackable();
// mc++                 if ((trackable instanceof Plane &&
// mc++                         ((Plane) trackable).isPoseInPolygon(hit.getHitPose()))) {
// mc++                     isHitting = true;
// mc++                     break;
// mc++                 }
// mc++             }
// mc++         }
// mc++         return wasHitting != isHitting;
// mc++     }
// mc++ 
// mc++     private android.graphics.Point getScreenCenter() {
// mc++         View vw = findViewById(android.R.id.content);
// mc++         return new android.graphics.Point(vw.getWidth() / 2, vw.getHeight() / 2);
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public boolean onCreateOptionsMenu(Menu menu) {
// mc++         // Inflate the menu; this adds items to the action bar if it is present.
// mc++         getMenuInflater().inflate(R.menu.menu_main, menu);
// mc++         return true;
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public boolean onOptionsItemSelected(MenuItem item) {
// mc++         // Handle action bar item clicks here. The action bar will
// mc++         // automatically handle clicks on the Home/Up button, so long
// mc++         // as you specify a parent activity in AndroidManifest.xml.
// mc++         int id = item.getItemId();
// mc++ 
// mc++         //noinspection SimplifiableIfStatement
// mc++         if (id == R.id.action_settings) {
// mc++             return true;
// mc++         }
// mc++ 
// mc++         return super.onOptionsItemSelected(item);
// mc++     }
// mc++     private void initializeGallery() {
// mc++         LinearLayout gallery = findViewById(R.id.gallery_layout);
// mc++ 
// mc++         ImageView andy = new ImageView(this);
// mc++         andy.setImageResource(R.drawable.droid_thumb);
// mc++         andy.setContentDescription("andy");
// mc++         andy.setOnClickListener(view ->{addObject( Uri.parse("andy.sfb"));});
// mc++         gallery.addView(andy);
// mc++ 
// mc++         ImageView cabin = new ImageView(this);
// mc++         cabin.setImageResource(R.drawable.cabin_thumb);
// mc++         cabin.setContentDescription("cabin");
// mc++         cabin.setOnClickListener(view ->{addObject(Uri.parse("Cabin.sfb"));});
// mc++         gallery.addView(cabin);
// mc++ 
// mc++         ImageView house = new ImageView(this);
// mc++         house.setImageResource(R.drawable.house_thumb);
// mc++         house.setContentDescription("house");
// mc++         house.setOnClickListener(view ->{addObject(Uri.parse("House.sfb"));});
// mc++         gallery.addView(house);
// mc++ 
// mc++         ImageView igloo = new ImageView(this);
// mc++         igloo.setImageResource(R.drawable.igloo_thumb);
// mc++         igloo.setContentDescription("igloo");
// mc++         igloo.setOnClickListener(view ->{addObject(Uri.parse("igloo.sfb"));});
// mc++         gallery.addView(igloo);
// mc++     }
// mc++     private void addObject(Uri model) {
// mc++         Frame frame = fragment.getArSceneView().getArFrame();
// mc++         Point pt = getScreenCenter();
// mc++         List<HitResult> hits;
// mc++         if (frame != null) {
// mc++             hits = frame.hitTest(pt.x, pt.y);
// mc++             for (HitResult hit : hits) {
// mc++                 Trackable trackable = hit.getTrackable();
// mc++                 if ((trackable instanceof Plane &&
// mc++                         ((Plane) trackable).isPoseInPolygon(hit.getHitPose()))) {
// mc++                     placeObject(fragment, hit.createAnchor(), model);
// mc++                     break;
// mc++ 
// mc++                 }
// mc++             }
// mc++         }
// mc++     }
// mc++     private void placeObject(ArFragment fragment, Anchor anchor, Uri model) {
// mc++         CompletableFuture<Void> renderableFuture =
// mc++                 ModelRenderable.builder()
// mc++                         .setSource(fragment.getContext(), model)
// mc++                         .build()
// mc++                         .thenAccept(renderable -> addNodeToScene(fragment, anchor, renderable))
// mc++                         .exceptionally((throwable -> {
// mc++                             AlertDialog.Builder builder = new AlertDialog.Builder(this);
// mc++                             builder.setMessage(throwable.getMessage())
// mc++                                     .setTitle("Codelab error!");
// mc++                             AlertDialog dialog = builder.create();
// mc++                             dialog.show();
// mc++                             return null;
// mc++                         }));
// mc++     }
// mc++ 
// mc++     private void addNodeToScene(ArFragment fragment, Anchor anchor, Renderable renderable) {
// mc++         AnchorNode anchorNode = new AnchorNode(anchor);
// mc++         TransformableNode node = new TransformableNode(fragment.getTransformationSystem());
// mc++         node.setRenderable(renderable);
// mc++         node.setParent(anchorNode);
// mc++         fragment.getArSceneView().getScene().addChild(anchorNode);
// mc++         node.select();
// mc++     }
// mc++ 
// mc++ }
