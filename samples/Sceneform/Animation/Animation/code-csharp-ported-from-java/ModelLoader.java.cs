// mc++ /*
// mc++  * Copyright 2018 Google LLC
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
// mc++ package com.google.ar.sceneform.samples.animation;
// mc++ 
// mc++ import android.util.Log;
// mc++ import android.util.SparseArray;
// mc++ import com.google.ar.sceneform.rendering.ModelRenderable;
// mc++ import java.lang.ref.WeakReference;
// mc++ import java.util.concurrent.CompletableFuture;
// mc++ 
// mc++ /**
// mc++  * Model loader class to avoid memory leaks from the activity. Activity and Fragment controller
// mc++  * classes have a lifecycle that is controlled by the UI thread. When a reference to one of these
// mc++  * objects is accessed by a background thread it is "leaked". Using that reference to a
// mc++  * lifecycle-bound object after Android thinks it has "destroyed" it can produce bugs. It also
// mc++  * prevents the Activity or Fragment from being garbage collected, which can leak the memory
// mc++  * permanently if the reference is held in the singleton scope.
// mc++  *
// mc++  * <p>To avoid this, use a non-nested class which is not an activity nor fragment. Hold a weak
// mc++  * reference to the activity or fragment and use that when making calls affecting the UI.
// mc++  */
// mc++ @SuppressWarnings({"AndroidApiChecker"})
// mc++ public class ModelLoader {
// mc++   private static final String TAG = "ModelLoader";
// mc++   private final SparseArray<CompletableFuture<ModelRenderable>> futureSet = new SparseArray<>();
// mc++   private final WeakReference<MainActivity> owner;
// mc++ 
// mc++   ModelLoader(MainActivity owner) {
// mc++     this.owner = new WeakReference<>(owner);
// mc++   }
// mc++ 
// mc++   /**
// mc++    * Starts loading the model specified. The result of the loading is returned asynchrounously via
// mc++    * {@link MainActivity#setRenderable(int, ModelRenderable)} or {@link
// mc++    * MainActivity#onException(int, Throwable)}.
// mc++    *
// mc++    * <p>Multiple models can be loaded at a time by specifying separate ids to differentiate the
// mc++    * result on callback.
// mc++    *
// mc++    * @param id the id for this call to loadModel.
// mc++    * @param resourceId the resource id of the .sfb to load.
// mc++    * @return true if loading was initiated.
// mc++    */
// mc++   boolean loadModel(int id, int resourceId) {
// mc++     MainActivity activity = owner.get();
// mc++     if (activity == null) {
// mc++       Log.d(TAG, "Activity is null.  Cannot load model.");
// mc++       return false;
// mc++     }
// mc++     CompletableFuture<ModelRenderable> future =
// mc++         ModelRenderable.builder()
// mc++             .setSource(owner.get(), resourceId)
// mc++             .build()
// mc++             .thenApply(renderable -> this.setRenderable(id, renderable))
// mc++             .exceptionally(throwable -> this.onException(id, throwable));
// mc++     if (future != null) {
// mc++       futureSet.put(id, future);
// mc++     }
// mc++     return future != null;
// mc++   }
// mc++ 
// mc++   ModelRenderable onException(int id, Throwable throwable) {
// mc++     MainActivity activity = owner.get();
// mc++     if (activity != null) {
// mc++       activity.onException(id, throwable);
// mc++     }
// mc++     futureSet.remove(id);
// mc++     return null;
// mc++   }
// mc++ 
// mc++   ModelRenderable setRenderable(int id, ModelRenderable modelRenderable) {
// mc++     MainActivity activity = owner.get();
// mc++     if (activity != null) {
// mc++       activity.setRenderable(id, modelRenderable);
// mc++     }
// mc++     futureSet.remove(id);
// mc++     return modelRenderable;
// mc++   }
// mc++ }
